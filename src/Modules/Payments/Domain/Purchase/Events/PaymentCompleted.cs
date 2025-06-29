using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public record PaymentCompleted(DateTimeOffset PurchasedAt) : StreamEventBase;
