using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public record SubscriptionCancelled(DateTimeOffset CanceledAt) : StreamEventBase;
