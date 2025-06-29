using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public record PaymentRefunded(DateTimeOffset RefundedAt) : StreamEventBase;
