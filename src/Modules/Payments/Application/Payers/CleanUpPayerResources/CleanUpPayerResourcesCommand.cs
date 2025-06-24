using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.CleanUpPayerResources;

public class CleanUpPayerResourcesCommand(
    PayerId payerId,
    string gatewayCustomerId,
    Guid id = default) : InternalCommandBase(id)
{
    public PayerId PayerId { get; } = payerId;
    public string GatewayCustomerId { get; } = gatewayCustomerId;
}
