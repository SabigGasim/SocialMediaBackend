using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.ActivateSubscription;

public sealed class SubscriptionActivatedNotification(
    AppSubscriptionActivatedDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<AppSubscriptionActivatedDomainEvent>(domainEvent, id);
