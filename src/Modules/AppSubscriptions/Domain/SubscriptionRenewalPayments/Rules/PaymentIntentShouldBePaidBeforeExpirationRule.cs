using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.Rules;

internal class PaymentIntentShouldBePaidBeforeExpirationRule(SubscriptionRenewalPayment payment) : IBusinessRule
{
    private readonly SubscriptionRenewalPayment _subscriptionRenewalPayment = payment;

    public string Message => "Cannot confirm an expired payment intent";

    public bool IsBroken() => _subscriptionRenewalPayment.PaymentStatus == SubscriptionPaymentStatus.Expired;

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
