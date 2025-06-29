using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public record PaymentCustomerCreated(string CustomerId) : StreamEventBase;