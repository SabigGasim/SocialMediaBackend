namespace SocialMediaBackend.Modules.Payments.Domain.Purchase;

public enum PaymentStatus
{
    Pending = 0,
    Incomplete = 100,
    Paid = 200,
    Refunded = 300,
    Canceled = 402
}
