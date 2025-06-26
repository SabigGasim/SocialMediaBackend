using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

public class PaymentRefnudedIntegrationEvent(
    Guid purchaseId,
    Guid payerId,
    string productReference,
    DateTimeOffset refnudedAt) : IntegrationEvent
{
    public Guid PurchaseId { get; } = purchaseId;
    public Guid PayerId { get; } = payerId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset RefnudedAt { get; } = refnudedAt;
}
