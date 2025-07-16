namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;

public enum SubscriptionStatus
{
    PastDue = 0,
    Canceled = 1,
    CancleAtPeriodEnd = 2,
    Active = 3,
}
