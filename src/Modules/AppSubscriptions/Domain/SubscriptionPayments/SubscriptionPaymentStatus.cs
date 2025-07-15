namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;

public enum SubscriptionPaymentStatus
{
    PaymentIntentRequested = 0,
    Expired = 1,
    Paid = 2,
}
