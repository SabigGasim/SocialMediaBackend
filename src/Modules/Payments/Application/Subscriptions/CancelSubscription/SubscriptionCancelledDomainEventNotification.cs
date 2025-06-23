using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;

public class SubscriptionCancelledDomainEventNotification(SubscriptionCancelledDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<SubscriptionCancelledDomainEvent>(domainEvent, id);
