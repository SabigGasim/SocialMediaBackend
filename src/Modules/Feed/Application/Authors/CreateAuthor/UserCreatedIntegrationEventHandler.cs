using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.CreateAuthor;

internal class UserCreatedIntegrationEventHandler : INotificationHandler<UserCreatedIntegrationEvent>
{
    public async ValueTask Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new CreateAuthorCommand(
                id: notification.Id,
                authorId: new AuthorId(notification.UserId),
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
