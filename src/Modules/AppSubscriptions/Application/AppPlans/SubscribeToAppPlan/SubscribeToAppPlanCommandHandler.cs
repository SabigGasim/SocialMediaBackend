using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.SubscribeToAppPlan;

internal sealed class SubscribeToAppPlanCommandHandler(
    IPaymentAntiCorruptionLayer paymentAntiCorruptionLayer,
    SubscriptionsDbContext context,
    IUserContext userContext) 
    : ICommandHandler<SubscribeToAppPlanCommand, CreateCheckoutSessionResponse>
{
    private readonly IPaymentAntiCorruptionLayer _paymentAntiCorruptionLayer = paymentAntiCorruptionLayer;
    private readonly SubscriptionsDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<HandlerResponse<CreateCheckoutSessionResponse>> ExecuteAsync(SubscribeToAppPlanCommand command, CancellationToken ct)
    {
        var subscriptionPaymentRequested = await _context.SubscriptionPayments
            .AsNoTracking()
            .AnyAsync(x => 
                x.PayerId == _userContext.UserId &&
                x.PaymentStatus == SubscriptionPaymentStatus.PaymentIntentRequested,
                ct);

        if (subscriptionPaymentRequested)
        {
            //TODO: Handle cancellation of previous payments and instantiation of new ones
            return ("You have already requested a subscription payment, please proceed with it", HandlerResponseStatus.Conflict);
        }

        var appPlan = await _context.AppSubscriptionProducts
            .AsNoTracking()
            .Where(x => x.Tier == command.AppPlanTier)
            .Include(x => x.Plans)
            .Select(x => x.Plans.First(x => x.Price.PaymentInterval == command.PaymentInterval))
            .FirstAsync(ct);

        var subscriptionPayment = SubscriptionPayment.CreatePaymentIntent(
            _userContext.UserId,
            appPlan.Id,
            DateTimeOffset.UtcNow.AddMinutes(30)); //TODO: get expiration time from PaymentACL. For now, we know it's 30m

        _context.SubscriptionPayments.Add(subscriptionPayment);

        return await _paymentAntiCorruptionLayer.CreateSubscriptionCheckoutSessionAsync(
            command.UserId,
            AppSubscriptionProduct.Reference,
            appPlan.Id.Value,
            successUrl: "https://localhost:7251/",
            cancelUrl: "https://localhost:7251/",
            ct);
    }
}
