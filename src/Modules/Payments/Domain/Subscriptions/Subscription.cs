using SocialMediaBackend.Modules.Payments.Domain.Common;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

public class Subscription : AggregateRoot
{
    public Guid PayerId { get; private set; }
    public string GatewaySubscriptionId { get; private set; } = default!;
    public string ProductReference { get; private set; } = default!;
    public SubscriptionStatus Status { get; private set; }
    public DateTimeOffset? ActivatedAt { get; private set; }
    public DateTimeOffset? ExpiresAt { get; private set; }

    private Subscription() { }

    public static Subscription Initiate(PayerId payerId, string productReference)
    {
        var subscription = new Subscription();
        
        var @event = new SubscriptionInitiated(SubscriptionId.New(), payerId, productReference);

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
            new(this.PayerId),
            new(this.Id),
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
            new(this.PayerId), 
            new(this.Id), 
            this.ProductReference)
            );
    }

    public void Apply(SubscriptionPastDue @event)
    {
        this.Status = SubscriptionStatus.PastDue;
    }

    public void Apply(SubscriptionCancelled @event)
    {
        this.Status = SubscriptionStatus.Cancelled;
        this.ActivatedAt = null;
        this.ExpiresAt = null;
    }

    public void Apply(SubscriptionMarkedIncomplete @event)
    {
        Status = SubscriptionStatus.Incomplete;
    }

    public void Apply(SubscriptionInitiated @event)
    {
        this.Id = @event.SubscriptionId.Value;
        this.PayerId = @event.PayerId.Value;
        this.ProductReference = @event.ProductReference;
        this.Status = SubscriptionStatus.Pending;
    }

    private void Apply(SubscriptionCreated @event)
    {
        this.GatewaySubscriptionId = @event.GatewaySubscriptionId;
        
        if (this.Status == SubscriptionStatus.Pending)
        {
            this.Status = SubscriptionStatus.Incomplete;
        }
    }

    public void Apply(SubscriptionActivated @event)
    {
        this.Status = SubscriptionStatus.Active;
        this.ActivatedAt = @event.ActivatedAt;
        this.ExpiresAt = @event.ExpiresAt;
    }
}