using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.RefundPayment;

public class PaymentRefundedDomainEventNotification(PaymentRefundedDomainEvent domainEvent, Guid id) 
    : DomainNotificationBase<PaymentRefundedDomainEvent>(domainEvent, id);
