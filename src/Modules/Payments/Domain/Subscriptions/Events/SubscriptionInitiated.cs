using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public record SubscriptionInitiated(SubscriptionId SubscriptionId, PayerId PayerId, string ProductReference) : StreamEventBase;
