using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.RenewSubscription;

public sealed class SubscriptionRenewedNotification(
    AppSubscriptionActivatedDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<AppSubscriptionActivatedDomainEvent>(domainEvent, id);
