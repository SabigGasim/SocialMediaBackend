using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public record PaymentCompleted(DateTimeOffset PurchasedAt) : StreamEventBase;
