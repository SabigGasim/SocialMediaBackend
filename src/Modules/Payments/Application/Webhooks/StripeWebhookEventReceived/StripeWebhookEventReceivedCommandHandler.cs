using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Domain.Exceptions;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsIncomplete;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsPastDue;
using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Webhooks.StripeWebhookEventReceived;

public class StripeWebhookEventReceivedCommandHandler(
    ICommandsScheduler scheduler) : ICommandHandler<StripeWebhookEventReceivedCommand>
{
    private readonly ICommandsScheduler _scheduler = scheduler;

    public async Task<HandlerResponse> ExecuteAsync(StripeWebhookEventReceivedCommand command, CancellationToken ct)
    {
        InternalCommandBase? commandToSchedule = command.Event.Type switch
        {
            Stripe.EventTypes.CustomerSubscriptionCreated => HandleSubscriptionCreated(command.Event),
            Stripe.EventTypes.CustomerSubscriptionUpdated => HandleSubscriptionUpdated(command.Event),
            Stripe.EventTypes.CustomerSubscriptionDeleted => HandleSubscriptionDeleted(command.Event),
            Stripe.EventTypes.InvoicePaid => HandleInvoicePaid(command.Event),
            _ => null
        };

        if (commandToSchedule is not null)
        {
            await _scheduler.EnqueueAsync(commandToSchedule, command.Event.Id);
        }

        return HandlerResponseStatus.OK;
    }

    /// <summary>
    /// Utilized to handle the case when a subscription succeeds renewal.
    /// We use this instead of customer.subscription.updated because it's easier to know
    /// that a new billing cycle has started, compared to checking previous attributes.
    /// </summary>
    private static FulfillSubscriptionCommand? HandleInvoicePaid(Stripe.Event @event)
    {
        var invoice = CreateDto((Stripe.Invoice)@event.Data.Object);

        if (invoice.Reason != BillingReasons.SubscriptionRenewal)
        {
            return null;
        }

        return new FulfillSubscriptionCommand(
            invoice.InternalSubscriptionId,
            SubscriptionStatus.Active,
            invoice.StartDate,
            invoice.ExpirationDate,
            JsonConvert.SerializeObject(@event));
    }

    private static InternalCommandBase? HandleSubscriptionCreated(Stripe.Event @event)
    {
        var subscription = CreateDto((Stripe.Subscription)@event.Data.Object);
        var eventJson = JsonConvert.SerializeObject(@event);

        return subscription.Status switch
        {
            SubscriptionStatus.Active => new FulfillSubscriptionCommand(
                subscription.InternalSubscriptionId,
                subscription.Status,
                subscription.StartDate,
                subscription.ExpirationDate,
                eventJson),
            SubscriptionStatus.Incomplete => new MarkSubscriptionAsIncompleteCommand(
                subscription.InternalSubscriptionId,
                eventJson),
            _ => null
        };
    }

    private static CancelSubscriptionCommand HandleSubscriptionDeleted(Stripe.Event @event)
    {
        var subscription = CreateShortDto((Stripe.Subscription)@event.Data.Object);

        return new CancelSubscriptionCommand(
            subscription.InternalSubscriptionId,
            JsonConvert.SerializeObject(@event));
    }

    /// <summary>
    /// Utilized to handle the case when a subscription fails renewal.
    /// This is useful as it gives info of the subscription status change.
    /// invoice.payment_failed would give the status of the payment, not the subscription.
    /// </summary>
    private static InternalCommandBase? HandleSubscriptionUpdated(Stripe.Event @event)
    {
        JObject previousAttributes = @event.Data.PreviousAttributes;

        if (!previousAttributes.ContainsKey("status"))
        {
            return null;
        }

        var subscription = CreateDto((Stripe.Subscription)@event.Data.Object);
        var eventJson = JsonConvert.SerializeObject(@event);

        return subscription.Status switch
        {
            SubscriptionStatus.Incomplete => new MarkSubscriptionAsIncompleteCommand(
                subscription.InternalSubscriptionId,
                eventJson),
            SubscriptionStatus.PastDue => new MarkSubscriptionAsPastDueCommand(
                subscription.InternalSubscriptionId,
                eventJson),
            _ => null
        };
    }

    private static ShortSubscriptionDto CreateShortDto(Stripe.Subscription subscription)
    {
        var internalSubscriptionId = subscription.Metadata.GetValueOrDefault("internal_subscription_id");

        return new ShortSubscriptionDto(
            InternalSubscriptionId: Guid.Parse(internalSubscriptionId!),
            Status: GetSubscriptionStatus(subscription.Status)
        );
    }

    private static SubscriptionDto CreateDto(Stripe.Subscription subscription)
    {
        var subscriptionItem = subscription.Items.Data[0];

        var (interval, count) = (subscriptionItem.Price.Recurring.Interval, subscriptionItem.Price.Recurring.IntervalCount);

        var paymentInterval = (interval, count) switch
        {
            ("week", 1) => PaymentInterval.Weekly,
            ("month", 1) => PaymentInterval.Monthly,
            ("month", 6) => PaymentInterval.HalfYear,
            ("year", 1) => PaymentInterval.Yearly,
            _ => throw new ThisWillNeverHappenException($"Unsupported payment interval: {interval}-{count}")
        };

        var internalSubscriptionId = subscription.Metadata.GetValueOrDefault("internal_subscription_id");

        return new SubscriptionDto(
            subscription.Id,
            Guid.Parse(internalSubscriptionId!),
            subscription.CustomerId,
            subscriptionItem.Price.Id,
            GetSubscriptionStatus(subscription.Status),
            new DateTimeOffset(subscriptionItem.CurrentPeriodStart, TimeSpan.Zero),
            new DateTimeOffset(subscriptionItem.CurrentPeriodEnd, TimeSpan.Zero),
            new ProductPrice(
                new MoneyValue(
                    amount: (int)subscriptionItem.Price.UnitAmount!,
                    currency: Enum.Parse<Currency>(subscriptionItem.Price.Currency, ignoreCase: true)),
                paymentInterval: paymentInterval
            )
        );
    }

    private static InvoiceDto CreateDto(Stripe.Invoice invoice)
    {
        var internalSubscriptionId = invoice.Parent.SubscriptionDetails.Metadata.GetValueOrDefault("internal_subscription_id");

        var subscriptionItem = invoice.Lines.Data[0];

        var billingReason = invoice.BillingReason switch
        {
            "subscription_cycle" => BillingReasons.SubscriptionRenewal,
            "subscription_create" => BillingReasons.SubscriptionCreated,
            _ => BillingReasons.Else
        };

        return new InvoiceDto(
            Guid.Parse(internalSubscriptionId!),
            billingReason,
            new DateTimeOffset(subscriptionItem.Period.Start, TimeSpan.Zero),
            new DateTimeOffset(subscriptionItem.Period.End, TimeSpan.Zero)
        );
    }

    private static SubscriptionStatus GetSubscriptionStatus(string status)
    {
        return status switch
        {
            Stripe.SubscriptionStatuses.Active or
            Stripe.SubscriptionStatuses.Trialing => SubscriptionStatus.Active,
            Stripe.SubscriptionStatuses.Canceled => SubscriptionStatus.Cancelled,
            Stripe.SubscriptionStatuses.PastDue => SubscriptionStatus.PastDue,
            _ => SubscriptionStatus.Incomplete
        };
    }
}

record SubscriptionDto(
    string Id, 
    Guid InternalSubscriptionId,
    string CustomerId, 
    string PriceId,
    SubscriptionStatus Status, 
    DateTimeOffset StartDate, 
    DateTimeOffset ExpirationDate, 
    ProductPrice Price);

enum BillingReasons
{
    SubscriptionCreated,
    SubscriptionRenewal,
    Else
}

record InvoiceDto(
    Guid InternalSubscriptionId, 
    BillingReasons Reason,
    DateTimeOffset StartDate,
    DateTimeOffset ExpirationDate);

record ShortSubscriptionDto(Guid InternalSubscriptionId, SubscriptionStatus Status);