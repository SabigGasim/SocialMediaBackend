using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.Events;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.RenewSubscription;

internal sealed class SubscriptionRenewalPaymentPaidDomainEventHandler(SubscriptionsDbContext context)
    : INotificationHandler<SubscriptionRenewalPaymentPaidDomainEvent>
{
    private readonly SubscriptionsDbContext _context = context;

    public async ValueTask Handle(SubscriptionRenewalPaymentPaidDomainEvent notification, CancellationToken cancellationToken)
    {
        var subscription = await _context.Subscriptions
            .Where(x => x.Id == notification.SubscriptionId)
            .FirstAsync(cancellationToken);

        subscription.Renew(
            notification.SubscriptionRenewedAt, 
            notification.SubscriptionExpiresAt);
    }
}
