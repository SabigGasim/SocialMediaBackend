using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.UpdateAuthorInfo;

public class UserInfoUpdatedIntegrationEventHandler : INotificationHandler<UserInforUpdatedIntegrationEvent>
{
    public async ValueTask Handle(UserInforUpdatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var command = new UpdateAuthorInfoCommand(
            notification.Id,
            notification.UserId,
            notification.Username,
            notification.Nickname,
            notification.ProfilePicture,
            notification.ProfileIsPublic
            );

        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            await scheduler.EnqueueAsync(command);
        }
    }
}
