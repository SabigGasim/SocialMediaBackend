using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.Rules;

internal class PaymentIntentShouldBePaidBeforeExpirationRule(SubscriptionPayment payment) : IBusinessRule
{
    private readonly SubscriptionPayment _subscriptionPayment = payment;

    public string Message => "Cannot confirm an expired payment intent";

    public bool IsBroken() => _subscriptionPayment.PaymentStatus == SubscriptionPaymentStatus.Expired;

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
