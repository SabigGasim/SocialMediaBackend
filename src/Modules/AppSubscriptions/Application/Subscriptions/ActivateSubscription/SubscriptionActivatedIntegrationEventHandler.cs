using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.ActivateSubscription;

internal sealed class SubscriptionActivatedIntegrationEventHandler(ICommandsScheduler scheduler)
    : INotificationHandler<SubscriptionActivatedIntegrationEvent>
{
    private readonly ICommandsScheduler _scheduler = scheduler;

    public async ValueTask Handle(SubscriptionActivatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.ProductReference != AppSubscriptionProduct.Reference)
        {
            return;
        }

        await _scheduler.EnqueueAsync(new CompleteSubscriptionPaymentCommand(
            notification.Id,
            new(notification.PayerId),
            new(notification.SubscriptionId),
            notification.ActivatedAt.ToUniversalTime(),
            notification.ActivatedAt.ToUniversalTime(),
            notification.ExpiresAt.ToUniversalTime()));
    }
}
