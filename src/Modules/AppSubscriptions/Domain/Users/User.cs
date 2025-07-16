using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    private readonly List<SubscriptionPayment> _subscriptionPayments = [];
    private readonly List<SubscriptionRenewalPayment> _subscriptionRenewalPayments = [];

    public IReadOnlyCollection<SubscriptionPayment> SubscriptionPayments => _subscriptionPayments.AsReadOnly();
    public IReadOnlyCollection<SubscriptionRenewalPayment> SubscriptionRenewalPayments => _subscriptionRenewalPayments.AsReadOnly();

    public Subscription? Subscription { get; private set; } = null;

    private User() {}
    public User(UserId userId) 
    {
        this.Id = userId;
    }
}
