using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public class PayerCreated(PayerId payerId) : IStreamEvent
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset OccuredOn { get; } = TimeProvider.System.GetUtcNow();
    public PayerId PayerId { get; } = payerId;
}
