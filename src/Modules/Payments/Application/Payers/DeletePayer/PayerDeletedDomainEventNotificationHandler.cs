using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Application.Payers.CleanUpPayerResources;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.DeletePayer;

internal sealed class PayerDeletedDomainEventNotificationHandler(ICommandsScheduler commandsScheduler)
    : INotificationHandler<PayerDeletedDomainEventNotification>
{
    private readonly ICommandsScheduler _scheduler = commandsScheduler;

    public async ValueTask Handle(PayerDeletedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _scheduler.EnqueueAsync(new CleanUpPayerResourcesCommand(
            notification.Event.PayerId,
            notification.Event.GatewayCustomerId,
            notification.Id)
            );
    }
}
