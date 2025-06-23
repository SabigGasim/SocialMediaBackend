using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;

public class SubscriptionActivatedDomainEventNotification(SubscriptionActivatedDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<SubscriptionActivatedDomainEvent>(domainEvent, id);
