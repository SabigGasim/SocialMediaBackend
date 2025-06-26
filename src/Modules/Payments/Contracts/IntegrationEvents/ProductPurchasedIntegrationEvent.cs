using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

public class ProductPurchasedIntegrationEvent(
    Guid PurchaseId,
    Guid PayerId,
    string productReference,
    DateTimeOffset PurchasedAt) : IntegrationEvent
{
    public Guid PurchaseId { get; } = PurchaseId;
    public Guid PayerId { get; } = PayerId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset PurchasedAt { get; } = PurchasedAt;
}
