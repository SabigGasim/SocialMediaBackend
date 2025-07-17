using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.CancelSubscription;

public sealed class AppSubscriptionCanceledNotification(
    AppSubscriptionCanceledDomainEvent domainEvent,
    Guid id) : DomainNotificationBase<AppSubscriptionCanceledDomainEvent>(domainEvent, id);
