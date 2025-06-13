using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public class PayerCreatedDomainEvent(PayerId payerId) : DomainEventBase
{
    public PayerId PayerId { get; } = payerId;
}
