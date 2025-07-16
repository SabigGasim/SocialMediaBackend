using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.Rules;

public class PaymentIntentShouldBePaidOnlyOnceRule(SubscriptionRenewalPayment payment) : IBusinessRule
{
    private readonly SubscriptionRenewalPayment _subscriptionRenewalPayment = payment;

    public string Message => "Cannot pay for the subscription twice";

    public bool IsBroken() => _subscriptionRenewalPayment.PaymentStatus == SubscriptionPaymentStatus.Paid;

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
