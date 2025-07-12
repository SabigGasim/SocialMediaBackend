using Autofac;
using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.CreateUser;

internal sealed class UserCreatedIntegrationEventHandler : INotificationHandler<UserCreatedIntegrationEvent>
{
    public async ValueTask Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = AppSubscriptionsCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new CreateUserCommand(
                id: notification.Id,
                userId: new UserId(notification.UserId)
                );

            await scheduler.EnqueueAsync(command);
        }
    }
}
