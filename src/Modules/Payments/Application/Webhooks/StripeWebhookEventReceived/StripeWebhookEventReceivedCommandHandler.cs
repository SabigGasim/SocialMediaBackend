using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;
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
            //Stripe.EventTypes.CustomerSubscriptionUpdated => HandleSubscriptionUpdated(command.Event),
            //Stripe.EventTypes.CustomerSubscriptionDeleted => HandleSubscriptionDeleted(command.Event),
            //Stripe.EventTypes.InvoicePaid => new PaymentIntentCreatedCommand(JsonConvert.DeserializeObject<PaymentIntent>(stripeObject)!),
            _ => null
        };

        if (commandToSchedule is null)
        {
            return ($"Stripe event {command.Event.Type} is not supported", HandlerResponseStatus.NotSupported);
        }

        await _scheduler.EnqueueAsync(commandToSchedule);

        return HandlerResponseStatus.OK;
    }

    private static FulfillSubscriptionCommand HandleSubscriptionCreated(Stripe.Event @event)
    {
        var subscription = CreateDto((Stripe.Subscription)@event.Data.Object);

        return subscription.Status switch
        {
            SubscriptionStatus.Active or SubscriptionStatus.Trialing => new FulfillSubscriptionCommand(
                subscription.InternalSubscriptionId,
                subscription.Status,
                subscription.StartDate,
                subscription.ExpirationDate,
                JsonConvert.SerializeObject(@event)),
            SubscriptionStatus.Incomplete => throw new NotImplementedException(),
            _ => throw new ArgumentException($"Subscription status {subscription.Status} is not supported for fulfillment.")
        };
    }

    private static SubscriptionDto CreateDto(Stripe.Subscription subscription)
    {
        var subscriptionItem = subscription.Items.Data.First();

        var (interval, count) = (subscriptionItem.Price.Recurring.Interval, subscriptionItem.Price.Recurring.IntervalCount);

        var paymentInterval = (interval, count) switch
        {
            ("week", 1) => PaymentInterval.Weekly,
            ("month", 1) => PaymentInterval.Monthly,
            ("month", 6) => PaymentInterval.HalfYear,
            ("year", 1) => PaymentInterval.Yearly,
            _ => throw new ArgumentException($"Unsupported payment interval: {interval}-{count}")
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

    private static SubscriptionStatus GetSubscriptionStatus(string status)
    {
        return status switch
        {
            Stripe.SubscriptionStatuses.Active => SubscriptionStatus.Active,
            Stripe.SubscriptionStatuses.Trialing => SubscriptionStatus.Trialing,
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