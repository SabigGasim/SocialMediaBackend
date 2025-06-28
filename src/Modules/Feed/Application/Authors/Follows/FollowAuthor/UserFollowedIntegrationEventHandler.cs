using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.Follows.FollowAuthor;
internal sealed class UserFollowedIntegrationEventHandler : INotificationHandler<UserFollowedIntegrationEvent>
{
    public async ValueTask Handle(UserFollowedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            await scheduler.EnqueueAsync(new FollowAuthorCommand(
                notification.Id,
                new AuthorId(notification.FollowerId),
                new AuthorId(notification.UserId),
                notification.FollowedAt));
        }
    }
}
