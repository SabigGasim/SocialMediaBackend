using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public record PurchaseInitiated(
    PurchaseId PurchaseId, 
    PayerId PayerId,
    string ProductReference) : StreamEventBase;
