using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Feed.Application.AppSubscriptions.AppSubscriptionCanceled;

internal sealed class AppSubscriptionCanceledIntegrationEventHandler(ICommandsScheduler scheduler)
    : INotificationHandler<AppSubscriptionCanceledIntegrationEvent>
{
    private readonly ICommandsScheduler _scheduler = scheduler;

    public async ValueTask Handle(AppSubscriptionCanceledIntegrationEvent notification, CancellationToken cancellationToken)
    {
        await _scheduler.EnqueueAsync(
            new CancelAppSubscriptionCommand(
                notification.Id,
                new(notification.SubscriberId))
            );
    }
}
