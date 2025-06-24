using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.DeletePayer;

public class PayerDeletedDomainEventNotification(PayerDeletedDomainEvent domainEvent, Guid id) 
    : DomainNotificationBase<PayerDeletedDomainEvent>(domainEvent, id);
