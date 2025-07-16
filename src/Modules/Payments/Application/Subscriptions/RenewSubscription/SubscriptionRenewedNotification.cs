using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.RenewSubscription;

public sealed class SubscriptionRenewedNotification(SubscriptionRenewedDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<SubscriptionRenewedDomainEvent>(domainEvent, id);
