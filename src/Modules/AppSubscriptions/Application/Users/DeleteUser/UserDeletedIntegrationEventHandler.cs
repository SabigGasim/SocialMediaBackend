using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.DeleteUser;

internal sealed class UserDeletedIntegrationEventHandler : INotificationHandler<UserDeletedIntegrationEvent>
{
    public async ValueTask Handle(UserDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = AppSubscriptionsCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new DeleteUserCommand(notification.Id, new UserId(notification.UserId));

            await scheduler.EnqueueAsync(command);
        }
    }
}
