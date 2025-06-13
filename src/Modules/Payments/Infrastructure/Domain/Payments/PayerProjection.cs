using Marten.Events.Aggregation;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Payments;

internal class PayerProjection : SingleStreamProjection<Payer, Guid>
{
    public void Apply(Payer payer, PayerCreated @event) => payer.Apply(@event);
    public void Apply(Payer payer, PayerDeleted @event) => payer.Apply(@event);
    public void Apply(Payer payer, PaymentCustomerCreated @event) => payer.Apply(@event);
}
