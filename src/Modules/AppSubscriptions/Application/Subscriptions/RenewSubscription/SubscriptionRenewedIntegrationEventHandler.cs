using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.RenewSubscription;

internal sealed class SubscriptionRenewedIntegrationEventHandler(ICommandsScheduler scheduler)
    : INotificationHandler<SubscriptionRenewedIntegrationEvent>
{
    private readonly ICommandsScheduler _scheduler = scheduler;

    public async ValueTask Handle(SubscriptionRenewedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        if (notification.ProductReference != AppSubscriptionProduct.Reference)
        {
            return;
        }

        await _scheduler.EnqueueAsync(new CompleteSubscriptionRenewalPaymentCommand(
            notification.Id,
            new(notification.PayerId),
            new(notification.SubscriptionId),
            notification.RenewedAt.ToUniversalTime(),
            notification.RenewedAt.ToUniversalTime(),
            notification.ExpiresAt.ToUniversalTime()));
    }
}
