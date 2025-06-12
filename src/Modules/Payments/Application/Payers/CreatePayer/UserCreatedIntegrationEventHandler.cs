using Autofac;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.CreatePayer;

public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    public async ValueTask Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        using (var scope = PaymentsCompositionRoot.BeginLifetimeScope())
        {
            var scheduler = scope.Resolve<ICommandsScheduler>();

            var command = new CreatePayerCommand(
                id: notification.Id,
                payerId: new PayerId(notification.UserId));

            await scheduler.EnqueueAsync(command);
        }
    }
}
