using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public class PayerDeletedDomainEvent(PayerId payerId) : DomainEventBase
{
    public PayerId PayerId { get; } = payerId;
}