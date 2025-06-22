using SocialMediaBackend.Modules.Payments.Domain.Common;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

public enum SubscriptionStatus
{
    Pending,
    Active,
    Processing,
    Incomplete,
    PastDue,
    Cancelled
}

public class Subscription : AggregateRoot
{
    public PayerId PayerId { get; private set; }
    public string GatewaySubscriptionId { get; private set; } = default!;
    public string ProductReference { get; private set; } = default!;
    public SubscriptionStatus Status { get; private set; }
    public DateTimeOffset? ActivatedAt { get; private set; }
    public DateTimeOffset? ExpiresAt { get; private set; }

    private Subscription() { }
    private Subscription(Guid id) => this.Id = id;

    public static Subscription Initiate(PayerId payerId, string productReference)
    {
        var subscription = new Subscription(Guid.CreateVersion7());
        
        var @event = new SubscriptionInitiated(payerId, productReference);

        subscription.Apply(@event);
        subscription.AddEvent(@event);

        return subscription;
    }

    public void Activate(DateTimeOffset expirationDate)
    {
        if (Status == SubscriptionStatus.Active)
        {
            return;
        }

        var @event = new SubscriptionActivated(Id, DateTimeOffset.UtcNow, expirationDate);

        this.Apply(@event);
        this.AddEvent(@event);
        this.AddDomainEvent(new SubscriptionActivatedDomainEvent(
            this.ProductReference,
            this.ActivatedAt!.Value,
            this.ExpiresAt!.Value)
            );
    }

    public void Apply(SubscriptionInitiated @event)
    {
        PayerId = @event.PayerId;
        ProductReference = @event.ProductReference;
        Status = SubscriptionStatus.Pending;
    }

    public void Apply(SubscriptionActivated @event)
    {
        Status = SubscriptionStatus.Active;
    }
}