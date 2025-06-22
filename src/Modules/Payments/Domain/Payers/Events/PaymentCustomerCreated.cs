using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public record PaymentCustomerCreated(string CustomerId) : StreamEventBase;