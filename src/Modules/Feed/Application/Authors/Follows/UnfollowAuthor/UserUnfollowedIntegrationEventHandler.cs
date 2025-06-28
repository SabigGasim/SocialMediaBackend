using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.Follows.UnfollowAuthor;

internal sealed class UserUnfollowedIntegrationEventHandler : INotificationHandler<UserUnfollowedIntegrationEvent>
{
    public async ValueTask Handle(UserUnfollowedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            await scheduler.EnqueueAsync(new UnfollowAuthorCommand(
                notification.Id,
                new AuthorId(notification.FollowerId),
                new AuthorId(notification.UserId)));
        }
    }
}
