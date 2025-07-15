using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.Rules;

public class PaymentIntentShouldBePaidOnlyOnceRule(SubscriptionPayment payment) : IBusinessRule
{
    private readonly SubscriptionPayment _subscriptionPayment = payment;

    public string Message => "Cannot pay for the subscription twice";

    public bool IsBroken() => _subscriptionPayment.PaymentStatus == SubscriptionPaymentStatus.Paid;

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
