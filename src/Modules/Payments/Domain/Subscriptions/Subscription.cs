using SocialMediaBackend.Modules.Payments.Domain.Common;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

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

    public void Activate(DateTimeOffset activationDate, DateTimeOffset expirationDate)
    {
        if (Status == SubscriptionStatus.Active &&
            ActivatedAt == activationDate &&
            ExpiresAt == expirationDate)
        {
            return;
        }

        var @event = new SubscriptionActivated(this.Id, activationDate, expirationDate);

        this.Apply(@event);
        this.AddEvent(@event);
        this.AddDomainEvent(new SubscriptionActivatedDomainEvent(
            this.PayerId,
            this.Id,
            this.ProductReference,
            this.ActivatedAt!.Value,
            this.ExpiresAt!.Value)
            );
    }

    public void MarkAsIncomplete()
    {
        if (Status == SubscriptionStatus.Incomplete)
        {
            return;
        }

        var @event = new SubscriptionMarkedIncomplete();

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void MarkAsPastDue()
    {
        if (Status == SubscriptionStatus.PastDue)
        {
            return;
        }

        var @event = new SubscriptionPastDue();

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void Cancel()
    {
        if (Status == SubscriptionStatus.Cancelled)
        {
            return;
        }

        var @event = new SubscriptionCancelled();

        this.Apply(@event);
        this.AddEvent(@event);
        this.AddDomainEvent(new SubscriptionCancelledDomainEvent(
            this.PayerId, 
            this.Id, 
            this.ProductReference)
            );
    }

    public void Apply(SubscriptionPastDue @event)
    {
        Status = SubscriptionStatus.PastDue;
    }

    public void Apply(SubscriptionCancelled @event)
    {
        Status = SubscriptionStatus.Cancelled;
        ActivatedAt = null;
        ExpiresAt = null;
    }

    public void Apply(SubscriptionMarkedIncomplete @event)
    {
        Status = SubscriptionStatus.Incomplete;
    }

    public void Apply(SubscriptionInitiated @event)
    {
        PayerId = @event.PayerId;
        ProductReference = @event.ProductReference;
        Status = SubscriptionStatus.Incomplete;
    }

    public void Apply(SubscriptionActivated @event)
    {
        Status = SubscriptionStatus.Active;
        ActivatedAt = @event.ActivatedAt;
        ExpiresAt = @event.ExpiresAt;
    }
}