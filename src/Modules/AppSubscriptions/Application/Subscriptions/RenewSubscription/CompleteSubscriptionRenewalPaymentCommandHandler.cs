using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.RenewSubscription;

internal sealed class CompleteSubscriptionRenewalPaymentCommandHandler(SubscriptionsDbContext context)
    : ICommandHandler<CompleteSubscriptionRenewalPaymentCommand>
{
    private readonly SubscriptionsDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(CompleteSubscriptionRenewalPaymentCommand command, CancellationToken ct)
    {
        var subscriptionRenewalPayment = await _context
            .SubscriptionRenewalPayments
            .Where(x => 
                x.PayerId == command.SubscriberId &&
                x.PaymentStatus == SubscriptionPaymentStatus.PaymentIntentRequested)
            .SingleOrDefaultAsync(ct);

        if (subscriptionRenewalPayment is null)
        {
            subscriptionRenewalPayment = SubscriptionRenewalPayment.CreatePaymentIntent(
                command.SubscriptionId,
                command.SubscriberId,
                command.SubscriptionExpiresAt);

            _context.SubscriptionRenewalPayments.Add(subscriptionRenewalPayment);
        }

        subscriptionRenewalPayment.MarkAsPaid(
            command.PaidAt,
            command.SubscriptionActivatedAt,
            command.SubscriptionExpiresAt);

        return HandlerResponseStatus.NoContent;
    }
}
