namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

public enum SubscriptionStatus
{
    Active,
    Incomplete,
    Trialing,
    PastDue,
    Cancelled
}
