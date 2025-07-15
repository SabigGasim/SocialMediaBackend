using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.ActivateSubscription;

internal sealed class CompleteSubscriptionPaymentCommandHandler(SubscriptionsDbContext context)
    : ICommandHandler<CompleteSubscriptionPaymentCommand>
{
    private readonly SubscriptionsDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(CompleteSubscriptionPaymentCommand command, CancellationToken ct)
    {
        var subscriptionPayment = await _context
            .SubscriptionPayments
            .Where(x => x.PayerId == command.SubscriberId
                && x.PaymentStatus == SubscriptionPaymentStatus.PaymentIntentRequested)
            .SingleAsync(ct);

        subscriptionPayment.MarkAsPaid(
            command.SubscriptionId,
            command.PaidAt,
            command.SubscriptionActivatedAt,
            command.SubscriptionExpiresAt);

        return HandlerResponseStatus.NoContent;
    }
}
