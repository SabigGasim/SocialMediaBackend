using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.CancelSubscription;

internal sealed class CancelSubscriptionCommandHandler(SubscriptionsDbContext context)
    : ICommandHandler<CancelSubscriptionCommand>
{
    private readonly SubscriptionsDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(CancelSubscriptionCommand command, CancellationToken ct)
    {
        var subscription = await _context.Subscriptions
            .Where(x => x.Id == command.SubscriptionId)
            .FirstAsync(ct);

        subscription.Cancel(command.CanceledAt);

        return HandlerResponseStatus.NoContent;
    }
}
