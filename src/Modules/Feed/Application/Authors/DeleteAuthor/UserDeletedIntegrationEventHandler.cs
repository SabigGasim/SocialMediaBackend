using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.DeleteAuthor;

public class UserDeletedIntegrationEventHandler : INotificationHandler<UserDeletedIntegrationEvent>
{
    public async ValueTask Handle(UserDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = FeedCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new DeleteAuthorCommand(notification.Id, new AuthorId(notification.UserId));

            await scheduler.EnqueueAsync(command);
        }
    }
}
