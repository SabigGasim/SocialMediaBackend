using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;
using SocialMediaBackend.Modules.Users.Domain.AppPlan;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.AppPlans.CreateAppSubscriptionProduct;

internal sealed class CreateAppSubscriptionProductCommandHandler(
    UsersDbContext context,
    IPaymentAntiCorruptionLayer paymentAntiCorruptionLayer) 
    : ICommandHandler<CreateAppSubscriptionProductCommand>
{
    private readonly UsersDbContext _context = context;
    private readonly IPaymentAntiCorruptionLayer _paymentAntiCorruptionLayer = paymentAntiCorruptionLayer;

    public async Task<HandlerResponse> ExecuteAsync(CreateAppSubscriptionProductCommand command, CancellationToken ct)
    {
        if (await _context.AppSubscriptionProducts.AnyAsync(x => x.Tier == command.Tier, ct))
        {
            return ($"App plan with tier '{command.Tier}' already exists.", HandlerResponseStatus.Conflict);
        }

        var (name, description) = AppSubscriptionProduct.GetProductDetails(command.Tier);

        var productId = await _paymentAntiCorruptionLayer.CreateProductAsync(
            AppSubscriptionProduct.Reference,
            name,
            description,
            owner: "system",
            ct);

        var product = AppSubscriptionProduct.Create(new(productId), command.Tier);

        _context.AppSubscriptionProducts.Add(product);

        return HandlerResponseStatus.Created;
    }
}
