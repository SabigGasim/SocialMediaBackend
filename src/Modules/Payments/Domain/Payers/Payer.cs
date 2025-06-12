using SocialMediaBackend.Modules.Payments.Domain.Common;
using SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers;

public class Payer : AggregateRoot
{
    private Payer() { }

    public bool IsDeleted { get; private set; }

    public static Payer Create(PayerCreated @event)
    {
        var payer = new Payer();
        payer.Apply(@event);
        payer.AddEvent(@event);

        return payer;
    }

    public void Apply(PayerCreated @event)
    {
        Id = @event.PayerId.Value;
        IsDeleted = false;
    }
    public void Apply(PayerDeleted @event) => IsDeleted = true;
}