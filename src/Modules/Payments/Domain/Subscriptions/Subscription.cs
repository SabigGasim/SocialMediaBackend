using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
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
    public DateTimeOffset? CanceledAt { get; private set; }

    private Subscription() { }

    public static Subscription Initiate(PayerId payerId, string productReference)
    {
        var subscription = new Subscription();
        
        var @event = new SubscriptionInitiated(SubscriptionId.New(), payerId, productReference);

        subscription.Apply(@event);
        subscription.AddEvent(@event);

        return subscription;
    }

    public void MarkCreated(string gatewaySubscriptionId)
    {
        var @event = new SubscriptionCreated(gatewaySubscriptionId);

        this.Apply(@event);
        this.AddEvent(@event);
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

    public void Renew(DateTimeOffset activationDate, DateTimeOffset expirationDate)
    {
        if (Status == SubscriptionStatus.Active &&
            ActivatedAt == activationDate &&
            ExpiresAt == expirationDate)
        {
            return;
        }

        var @event = new SubscriptionRenewed(this.Id, activationDate, expirationDate);

        this.Apply(@event);
        this.AddEvent(@event);
        this.AddDomainEvent(new SubscriptionRenewedDomainEvent(
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

    public void CancelAtPeriodEnd()
    {
        if (this.Status is not SubscriptionStatus.Active or SubscriptionStatus.PastDue)
        {
            return;
        }

        var @event = new AssigentToCancelAtPeriodEnd();

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void Reactivate()
    {
        if (this.Status != SubscriptionStatus.CancelAtPeriodEnd)
        {
            return;
        }

        var @event = new SubscriptionReactivated();

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void Cancel(DateTimeOffset canceledAt)
    {
        if (Status == SubscriptionStatus.Cancelled)
        {
            return;
        }

        var @event = new SubscriptionCancelled(canceledAt);

        this.Apply(@event);
        this.AddEvent(@event);
        if (this.Status != SubscriptionStatus.Pending)
        {
            this.AddDomainEvent(new SubscriptionCancelledDomainEvent(
                new(this.PayerId),
                new(this.Id),
                this.ProductReference,
                this.CanceledAt!.Value)
                );
        }
    }

    public void Apply(SubscriptionPastDue @event)
    {
        this.Status = SubscriptionStatus.PastDue;
    }

    public void Apply(SubscriptionCancelled @event)
    {
        this.Status = SubscriptionStatus.Cancelled;
        this.CanceledAt = @event.CanceledAt;
    }

    public void Apply(SubscriptionMarkedIncomplete @event)
    {
        this.Status = SubscriptionStatus.Incomplete;
    }

    public void Apply(SubscriptionInitiated @event)
    {
        this.Id = @event.SubscriptionId.Value;
        this.PayerId = @event.PayerId.Value;
        this.ProductReference = @event.ProductReference;
        this.Status = SubscriptionStatus.Pending;
    }

    public void Apply(SubscriptionCreated @event)
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

    public void Apply(SubscriptionRenewed @event)
    {
        this.Status = SubscriptionStatus.Active;
        this.ActivatedAt = @event.ActivatedAt;
        this.ExpiresAt = @event.ExpiresAt;
    }

    public void Apply(AssigentToCancelAtPeriodEnd @event)
    {
        this.Status = SubscriptionStatus.CancelAtPeriodEnd;
    }

    public void Apply(SubscriptionReactivated @event)
    {
        this.Status = SubscriptionStatus.Active;
    }
}