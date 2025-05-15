using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.CreateChatter;

public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    public async ValueTask Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new CreateChatterCommand(
                id: notification.Id,
                chatterId: new ChatterId(notification.UserId),
                notification.Username,
                notification.Nickname,
                notification.ProfilePicture,
                notification.ProfileIsPublic,
                notification.FollowersCount,
                notification.FollowingCount);

            await scheduler.EnqueueAsync(command);
        }
    }
}
