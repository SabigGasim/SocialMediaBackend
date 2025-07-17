using Newtonsoft.Json.Linq;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Domain.Helpers;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Application.Payments.CancelPurchase;
using SocialMediaBackend.Modules.Payments.Application.Payments.FulfillPurchase;
using SocialMediaBackend.Modules.Payments.Application.Payments.ProcessPaymentCreated;
using SocialMediaBackend.Modules.Payments.Application.Payments.RefundPayment;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsIncomplete;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsPastDue;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.ProcessSubscriptionCreated;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.RenewSubscription;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Webhooks.StripeWebhookEventReceived;

internal sealed class StripeWebhookEventReceivedCommandHandler(
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
            Stripe.EventTypes.CheckoutSessionExpired => HandleCheckoutSessionExpired(command.Event),
            Stripe.EventTypes.PaymentIntentCreated => HandlePaymentIntentCreated(command.Event),
            Stripe.EventTypes.PaymentIntentSucceeded => HandlePaymentIntentSucceded(command.Event),
            Stripe.EventTypes.PaymentIntentPaymentFailed => HandlePaymentIntentFailed(command.Event),
            Stripe.EventTypes.ChargeRefunded => HandleChargeRefunded(command.Event),
            Stripe.EventTypes.InvoicePaid => HandleInvoicePaid(command.Event),
            _ => null
        };

        if (commandToSchedule is not null)
        {
            await _scheduler.EnqueueAsync(commandToSchedule, command.Event.Id);
        }

        return HandlerResponseStatus.OK;
    }

    private static RefundPaymentCommand HandleChargeRefunded(Stripe.Event @event)
    {
        var charge = CreateDto((Stripe.Charge)@event.Data.Object);

        return new RefundPaymentCommand(new PurchaseId(charge.InternalId), charge.RefundedAt);
    }

    public static CancelPurchaseCommand? HandlePaymentIntentFailed(Stripe.Event @event)
    {
        var paymentIntent = CreateDto((Stripe.PaymentIntent)@event.Data.Object);

        return paymentIntent.PaymentMode == PaymentMode.Payment
            ? new CancelPurchaseCommand(new(paymentIntent.PaymentId))
            : null;
    }

    public static FulfillPurchaseCommand? HandlePaymentIntentSucceded(Stripe.Event @event)
    {
        var paymentIntent = CreateDto((Stripe.PaymentIntent)@event.Data.Object);

        return paymentIntent.PaymentMode == PaymentMode.Payment
            ? new FulfillPurchaseCommand(
                new(paymentIntent.PaymentId),
                paymentIntent.PaidAt)
            : null;
    }

    public static InternalCommandBase? HandlePaymentIntentCreated(Stripe.Event @event)
    {
        var paymentIntent = CreateDto((Stripe.PaymentIntent)@event.Data.Object);

        return paymentIntent.PaymentMode == PaymentMode.Payment
            ? new ProcessPaymentCreatedCommand(
                new(paymentIntent.PaymentId),
                new(paymentIntent.GatewayPaymentId))
            : null;
    }

    /// <summary>
    /// Utilized to handle the case when a subscription is created or succeeds renewal.
    /// We use this instead of customer.subscription.updated because it's easier to know
    /// that a new billing cycle has started, compared to checking previous attributes.
    /// We use this instead of customer.subscription.created to dodge the case when
    /// a subscription is created with status of incomplete. Typically, when
    /// payment_behavior=default_incomplete.
    /// </summary>
    private static InternalCommandBase? HandleInvoicePaid(Stripe.Event @event)
    {
        var invoice = CreateDto((Stripe.Invoice)@event.Data.Object);

        return invoice.Reason switch
        {
            BillingReasons.SubscriptionCreated => new FulfillSubscriptionCommand(
                invoice.InternalSubscriptionId,
                invoice.StartDate,
                invoice.ExpirationDate,
                @event.Id),
            BillingReasons.SubscriptionRenewal => new RenewSubscriptionCommand(
                invoice.InternalSubscriptionId,
                invoice.StartDate,
                invoice.ExpirationDate,
                @event.Id),
            _ => null
        };
    }

    private static ProcessSubscriptionCreatedCommand HandleSubscriptionCreated(Stripe.Event @event)
    {
        var subscription = CreateShortDto((Stripe.Subscription)@event.Data.Object);

        return new ProcessSubscriptionCreatedCommand(
            subscription.InternalSubscriptionId,
            subscription.SubscriptionId,
            @event.Id);
    }

    private static CancelSubscriptionCommand HandleSubscriptionDeleted(Stripe.Event @event)
    {
        var subscription = CreateShortDto((Stripe.Subscription)@event.Data.Object);

        return new CancelSubscriptionCommand(
            subscription.InternalSubscriptionId,
            @event.Id,
            subscription.CanceledAt!.Value);
    }

    private static CancelSubscriptionCommand HandleCheckoutSessionExpired(Stripe.Event @event)
    {
        var session = CreateDto((Stripe.Checkout.Session)@event.Data.Object);

        return new CancelSubscriptionCommand(
            session.InternalSubscriptionId,
            @event.Id,
            DateTimeOffset.UtcNow);
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

        var subscription = CreateShortDto((Stripe.Subscription)@event.Data.Object);

        return subscription.Status switch
        {
            SubscriptionStatus.Incomplete => new MarkSubscriptionAsIncompleteCommand(
                subscription.InternalSubscriptionId,
                @event.Id),
            SubscriptionStatus.PastDue => new MarkSubscriptionAsPastDueCommand(
                subscription.InternalSubscriptionId,
                @event.Id),
            _ => null
        };
    }

    private static ShortSubscriptionDto CreateShortDto(Stripe.Subscription subscription)
    {
        var internalSubscriptionId = subscription.Metadata.GetValueOrDefault("internal_subscription_id");

        return new ShortSubscriptionDto(
            Guid.Parse(internalSubscriptionId!),
            subscription.Id,
            Status: GetSubscriptionStatus(subscription.Status),
            subscription.CanceledAt?.ToUtcDateTimeOffset()
        );
    }

    private static PaymentIntentDto CreateDto(Stripe.PaymentIntent paymentIntent)
    {
        var mode = paymentIntent.Metadata.GetValueOrDefault("mode") == "payment"
            ? PaymentMode.Payment
            : PaymentMode.Subscription;

        var id = mode == PaymentMode.Payment
            ? Guid.Parse(paymentIntent.Metadata.GetValueOrDefault("internal_payment_id")!)
            : Guid.Empty;

        return new PaymentIntentDto(mode, paymentIntent.Id, id, DateTimeOffset.UtcNow);
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
            subscriptionItem.Period.Start.ToUtcDateTimeOffset(),
            subscriptionItem.Period.End.ToUtcDateTimeOffset()
        );
    }

    private static SessionDto CreateDto(Stripe.Checkout.Session session)
    {
        return new SessionDto(Guid.Parse(session.Metadata["internal_subscription_id"].ToString()));
    }

    private static ChargeDto CreateDto(Stripe.Charge charge)
    {
        return new ChargeDto(
            Guid.Parse(charge.Metadata.GetValueOrDefault("internal_payment_id")!),
            DateTimeOffset.UtcNow);
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

enum BillingReasons
{
    SubscriptionCreated,
    SubscriptionRenewal,
    Else
}

enum PaymentMode
{
    Payment, Subscription
}

record InvoiceDto(
    Guid InternalSubscriptionId, 
    BillingReasons Reason,
    DateTimeOffset StartDate,
    DateTimeOffset ExpirationDate);

record PaymentIntentDto(
    PaymentMode PaymentMode,
    string GatewayPaymentId,
    Guid PaymentId,
    DateTimeOffset PaidAt
    );

record SessionDto(Guid InternalSubscriptionId);

record ChargeDto(Guid InternalId, DateTimeOffset RefundedAt);

record ShortSubscriptionDto(
    Guid InternalSubscriptionId, 
    string SubscriptionId, 
    SubscriptionStatus Status,
    DateTimeOffset? CanceledAt = default);