using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.DeletePayer;

public class UserDeletedIntegrationEventHandler : IIntegrationEventHandler<UserDeletedIntegrationEvent>
{
    public async ValueTask Handle(UserDeletedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new DeletePayerCommand(
                id: notification.Id,
                payerId: new PayerId(notification.UserId));

            await scheduler.EnqueueAsync(command);
        }
    }
}
