using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppSubscriptionProduct;

internal sealed class CreateAppSubscriptionProductCommandHandler(
    SubscriptionsDbContext context,
    IPaymentAntiCorruptionLayer paymentAntiCorruptionLayer) 
    : ICommandHandler<CreateAppSubscriptionProductCommand>
{
    private readonly SubscriptionsDbContext _context = context;
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
