using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.Follows.FollowChatter;
public class UserFollowedIntegrationEventHandler : INotificationHandler<UserFollowedIntegrationEvent>
{
    public async ValueTask Handle(UserFollowedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            await scheduler.EnqueueAsync(new FollowChatterCommand(
                notification.Id,
                new ChatterId(notification.FollowerId),
                new ChatterId(notification.UserId),
                notification.FollowedAt));
        }
    }
}
