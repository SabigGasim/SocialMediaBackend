using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.Payments.Application.Webhooks.StripeWebhookEventReceived;

public class StripeWebhookEventReceivedCommandHandler(
    ICommandsScheduler scheduler) : ICommandHandler<StripeWebhookEventReceivedCommand>
{
    private readonly ICommandsScheduler _scheduler = scheduler;

    public async Task<HandlerResponse> ExecuteAsync(StripeWebhookEventReceivedCommand command, CancellationToken ct)
    {
        var stripeObject = command.Event.Object;
        var eventType = command.Event.Type;

        InternalCommandBase? commandToSchedule = command.Event.Type switch
        {
            //Stripe.EventTypes.CustomerSubscriptionCreated => HandleSubscriptionCreated(command.Event),
            //Stripe.EventTypes.CustomerSubscriptionDeleted => JsonConvert.DeserializeObject<PaymentIntent>(stripeObject)!,
            //Stripe.EventTypes.CustomerSubscriptionUpdated => new PaymentIntentCreatedCommand(JsonConvert.DeserializeObject<PaymentIntent>(stripeObject)!),
            //Stripe.EventTypes.CustomerSubscriptionPaused => new PaymentIntentCreatedCommand(JsonConvert.DeserializeObject<PaymentIntent>(stripeObject)!),
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

        return new FulfillSubscriptionCommand(
            subscription.Id,
            subscription.CustomerId,
            subscription.PriceId,
            subscription.StartDate,
            subscription.ExpirationDate
        );
    }

    private static SubscriptionDto CreateDto(Stripe.Subscription subscription)
    {
        var subscriptionItem = subscription.Items.Data.First();

        var (interval, count) = (subscriptionItem.Price.Recurring.Interval, subscriptionItem.Price.Recurring.IntervalCount);

        var paymentInterval = (interval, count) switch
        {
            ("month", 1) => PaymentInterval.Monthly,
            ("month", 6) => PaymentInterval.HalfYear,
            ("year", 1) => PaymentInterval.Yearly,
            _ => throw new ArgumentException($"Unsupported payment interval: {interval}-{count}")
        };

        return new SubscriptionDto(
            subscription.Id,
            subscription.CustomerId,
            subscriptionItem.Price.Id,
            subscription.Status,
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
}

record SubscriptionDto(
    string Id, 
    string CustomerId, 
    string PriceId, 
    string Status, 
    DateTimeOffset StartDate, 
    DateTimeOffset ExpirationDate, 
    ProductPrice Price);