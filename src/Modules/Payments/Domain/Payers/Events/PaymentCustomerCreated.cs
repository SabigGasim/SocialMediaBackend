using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public class PaymentCustomerCreated(string customerId) : IStreamEvent
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset OccuredOn { get; } = TimeProvider.System.GetUtcNow();
    public string CustomerId { get; } = customerId;
}
