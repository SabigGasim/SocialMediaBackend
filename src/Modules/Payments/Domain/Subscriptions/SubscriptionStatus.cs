namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

public enum SubscriptionStatus
{
    Pending = 0,
    Active = 200,
    Incomplete = 400,
    PastDue = 401,
    Cancelled = 402
}
