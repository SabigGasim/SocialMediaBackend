using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.SubscribeToAppPlan;

internal sealed class SubscribeToAppPlanCommandHandler(
    IPaymentAntiCorruptionLayer paymentAntiCorruptionLayer,
    SubscriptionsDbContext context) 
    : ICommandHandler<SubscribeToAppPlanCommand, CreateCheckoutSessionResponse>
{
    private readonly IPaymentAntiCorruptionLayer _paymentAntiCorruptionLayer = paymentAntiCorruptionLayer;
    private readonly SubscriptionsDbContext _context = context;

    public async Task<HandlerResponse<CreateCheckoutSessionResponse>> ExecuteAsync(SubscribeToAppPlanCommand command, CancellationToken ct)
    {
        var product = await _context.AppSubscriptionProducts
            .AsNoTracking()
            .Where(x => x.Tier == command.AppPlanTier)
            .Include(x => x.Plans)
            .FirstAsync(ct);

        var appPlan = product.Plans.First(x => x.Price.PaymentInterval == command.PaymentInterval);

        return await _paymentAntiCorruptionLayer.CreateSubscriptionCheckoutSessionAsync(
            command.UserId,
            AppSubscriptionProduct.Reference,
            appPlan.Id.Value,
            successUrl: "https://localhost:7251/",
            cancelUrl: "https://localhost:7251/",
            ct);
    }
}
