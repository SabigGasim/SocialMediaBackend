using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public record PurchasePaymentCreated(string GatewayPaymentId) : StreamEventBase;
