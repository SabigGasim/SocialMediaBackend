using SocialMediaBackend.Modules.Payments.Domain.Common;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase;

public class Purchase : AggregateRoot
{
    public Guid PayerId { get; private set; }
    public Guid ProductId { get; private set; }
    public string PaymentGatewayId { get; private set; } = default!;
    public PaymentStatus PaymentStatus { get; private set; }
    public string ProductReference { get; private set; } = default!;
    public DateTimeOffset? PurchasedAt { get; private set; }
    public DateTimeOffset? RefundedAt { get; private set; }

    private Purchase() { }

    public static Purchase Initiate(PayerId payerId, string productReference)
    {
        var purchase = new Purchase();

        var @event = new PurchaseInitiated(PurchaseId.New(), payerId, productReference);

        purchase.Apply(@event);
        purchase.AddEvent(@event);

        return purchase;
    }

    public void MarkCreated(string gatewayPaymentId)
    {
        var @event = new PurchasePaymentCreated(gatewayPaymentId);

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void Fulfill(DateTimeOffset purchasedAt)
    {
        var @event = new PaymentCompleted(purchasedAt);

        if (this.PaymentStatus >= PaymentStatus.Paid)
        {
            return;
        }

        this.Apply(@event);
        this.AddEvent(@event);
        this.AddDomainEvent(new ProductPurchasedDomainEvent(
            new(this.Id),
            new(this.PayerId),
            this.ProductReference,
            this.PurchasedAt!.Value)
            );
    }

    public void Cancel()
    {
        var @event = new PaymentCanceled();

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void Refund(DateTimeOffset refundedAt)
    {
        var @event = new PaymentRefunded(refundedAt);

        this.Apply(@event);
        this.AddEvent(@event);
        this.AddDomainEvent(new PaymentRefundedDomainEvent(
            new(this.Id),
            new(this.PayerId),
            this.ProductReference,
            this.RefundedAt!.Value)
            );
    }

    public void Apply(PaymentRefunded @event)
    {
        this.PaymentStatus = PaymentStatus.Refunded;
        this.RefundedAt = @event.RefundedAt;
    }

    public void Apply(PaymentCanceled @event)
    {
        this.PaymentStatus = PaymentStatus.Canceled;
    }

    public void Apply(PaymentCompleted @event)
    {
        this.PaymentStatus = PaymentStatus.Paid;
    }

    public void Apply(PurchasePaymentCreated @event)
    {
        this.PaymentGatewayId = @event.GatewayPaymentId;
        this.PaymentStatus = PaymentStatus.Incomplete;
    }

    public void Apply(PurchaseInitiated @event)
    {
        this.Id = @event.PurchaseId.Value;
        this.PayerId = @event.PayerId.Value;
        this.ProductReference = @event.ProductReference;
        this.PaymentStatus = PaymentStatus.Pending;
    }
}
