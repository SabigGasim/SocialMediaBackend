using Autofac;
using Mediator;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.CreatePayer;

internal class UserCreatedIntegrationEventHandler(IHostEnvironment env) : INotificationHandler<UserCreatedIntegrationEvent>
{
    private readonly IHostEnvironment _env = env;

    public async ValueTask Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (_env.IsEnvironment("Testing"))
        {
            return;
        }

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
