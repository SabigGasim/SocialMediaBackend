using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public class PayerDeletedDomainEvent(PayerId payerId, string gatewayCustomerId) : DomainEventBase
{
    public PayerId PayerId { get; } = payerId;
    public string GatewayCustomerId { get; } = gatewayCustomerId;
}