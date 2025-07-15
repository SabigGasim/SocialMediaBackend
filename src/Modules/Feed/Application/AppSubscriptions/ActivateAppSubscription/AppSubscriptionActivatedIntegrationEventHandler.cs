using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;

namespace SocialMediaBackend.Modules.Feed.Application.AppSubscriptions.ActivateAppSubscription;

internal sealed class AppSubscriptionActivatedIntegrationEventHandler(ICommandsScheduler scheduler)
    : INotificationHandler<AppSubscriptionActivatedIntegrationEvent>
{
    private readonly ICommandsScheduler _scheduler = scheduler;

    public async ValueTask Handle(AppSubscriptionActivatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _scheduler.EnqueueAsync(new ActivateAppSubscriptionCommand(
            notification.Id,
            new(notification.SubscriberId),
            notification.SubscriptionTier)
            );
    }
}
