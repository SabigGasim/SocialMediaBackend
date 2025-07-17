using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.CancelSubscription;

internal sealed class SubscriptionCanceledIntegrationEventHandler(ICommandsScheduler scheduler)
    : INotificationHandler<SubscriptionCancelledIntegrationEvent>
{
    private readonly ICommandsScheduler _scheduler = scheduler;

    public async ValueTask Handle(SubscriptionCancelledIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.ProductReference != AppSubscriptionProduct.Reference)
        {
            return;
        }

        await _scheduler.EnqueueAsync(
            new CancelSubscriptionCommand(
                notification.Id,
                new(notification.SubscriptionId),
                notification.CanceledAt.ToUniversalTime())
            );
    }
}
