using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.ProcessPaymentCreated;

public class ProcessPaymentCreatedCommand(
    PurchaseId purchaseId,
    string paymentGatewayId,
    Guid id = default) : InternalCommandBase(id)
{
    public PurchaseId PurchaseId { get; } = purchaseId;
    public string PaymentGatewayId { get; } = paymentGatewayId;
}
