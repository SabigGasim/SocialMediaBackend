using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public class SubscriptionActivatedDomainEvent(
    string productReference,
    DateTimeOffset ActivatedAt,
    DateTimeOffset ExpiresAt) : DomainEventBase
{
    public string ProductReference { get; } = productReference;
    public DateTimeOffset ActivatedAt { get; } = ActivatedAt;
    public DateTimeOffset ExpiresAt { get; } = ExpiresAt;
}
