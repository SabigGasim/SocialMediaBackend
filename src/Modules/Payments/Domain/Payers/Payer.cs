using SocialMediaBackend.Modules.Payments.Domain.Common;
using SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers;

public class Payer : AggregateRoot
{
    private Payer() { }

    private Payer(PayerId payerId) 
    {
        var @event = new PayerCreated(payerId);

        this.Apply(@event);
        this.AddEvent(@event);

        this.AddDomainEvent(new PayerCreatedDomainEvent(payerId));
    }

    public bool IsDeleted { get; private set; }

    public static Payer Create(PayerId payerId)
    {
        return new Payer(payerId);
    }


    public void Apply(PayerCreated @event)
    {
        Id = @event.PayerId.Value;
        IsDeleted = false;
    }
    public void Apply(PayerDeleted @event) => IsDeleted = true;
}