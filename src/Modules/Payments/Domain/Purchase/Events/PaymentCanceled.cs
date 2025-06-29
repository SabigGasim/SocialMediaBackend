using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public record PaymentCanceled : StreamEventBase;
