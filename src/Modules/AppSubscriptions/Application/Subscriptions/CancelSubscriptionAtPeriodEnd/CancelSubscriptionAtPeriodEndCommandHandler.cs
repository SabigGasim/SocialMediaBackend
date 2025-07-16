using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.CancelSubscriptionAtPeriodEnd;
internal sealed class CancelSubscriptionAtPeriodEndCommandHandler(
    SubscriptionsDbContext context,
    IUserContext userContext,
    IPaymentAntiCorruptionLayer paymentAntiCorruptionLayer)
    : ICommandHandler<CancelSubscriptionAtPeriodEndCommand>
{
    private readonly SubscriptionsDbContext _context = context;
    private readonly IUserContext _userContext = userContext;
    private readonly IPaymentAntiCorruptionLayer _paymentAntiCorruptionLayer = paymentAntiCorruptionLayer;

    public async Task<HandlerResponse> ExecuteAsync(CancelSubscriptionAtPeriodEndCommand command, CancellationToken ct)
    {
        var subscription = await _context
            .Subscriptions
            .Where(x => x.SubscriberId == _userContext.UserId)
            .FirstOrDefaultAsync(ct);

        if (subscription is null)
        {
            return ("User is not subscribed to any plan", HandlerResponseStatus.NotFound);
        }

        var cancelAtPeriodEndResult = subscription.CancelAtPeriodEnd(DateTimeOffset.UtcNow);
        if (!cancelAtPeriodEndResult.IsSuccess)
        {
            return cancelAtPeriodEndResult; 
        }

        return await _paymentAntiCorruptionLayer.CancelSubscriptionAtPeriodEnd(subscription.Id.Value, ct);
    }
}
