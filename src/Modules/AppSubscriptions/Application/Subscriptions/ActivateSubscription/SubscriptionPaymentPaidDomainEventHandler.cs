using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.Events;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.ActivateSubscription;

internal sealed class SubscriptionPaymentPaidDomainEventHandler(SubscriptionsDbContext context)
    : INotificationHandler<SubscriptionPaymentPaidDomainEvent>
{
    private readonly SubscriptionsDbContext _context = context;

    public async ValueTask Handle(SubscriptionPaymentPaidDomainEvent notification, CancellationToken cancellationToken)
    {
        var subscriptionTier = await _context
            .AppSubscriptionProducts
            .AsNoTracking()
            .Include(x => x.Plans)
            .Where(x => x.Plans.Any(x => x.Id == notification.AppSubscriptionPlanId))
            .Select(x => x.Tier)
            .FirstAsync(CancellationToken.None);

        var subscription = Subscription.CreateActive(
            notification.SubscriptionId,
            notification.PayerId,
            subscriptionTier,
            notification.SubscriptionActivatedAt,
            notification.SubscriptionExpiresAt);

        var oldSubscription = await _context.Subscriptions
            .Where(x => 
                x.SubscriberId == notification.PayerId &&
                x.Status == SubscriptionStatus.Canceled)
            .FirstOrDefaultAsync(CancellationToken.None);

        if (oldSubscription is not null)
        {
            _context.Subscriptions.Remove(oldSubscription);
        }

        _context.Subscriptions.Add(subscription);
    }
}
