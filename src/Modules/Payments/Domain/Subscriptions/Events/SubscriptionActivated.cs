using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public record SubscriptionActivated(
    Guid SubscriptionId,
    DateTimeOffset ActivatedAt,
    DateTimeOffset ExpiresAt
) : StreamEventBase;
