using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public record SubscriptionRenewed(
    Guid SubscriptionId,
    DateTimeOffset ActivatedAt,
    DateTimeOffset ExpiresAt
) : StreamEventBase;
