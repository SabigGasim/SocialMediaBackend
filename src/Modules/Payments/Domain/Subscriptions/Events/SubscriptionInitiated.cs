using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public record SubscriptionInitiated(PayerId PayerId, string ProductReference) : StreamEventBase;
