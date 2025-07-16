using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscriptionAtPeriodEnd;

internal sealed class CancelSubscriptionAtPeriodEndCommandHandler(
    IAggregateRepository repository,
    IPaymentService paymentService)
    : ICommandHandler<CancelSubscriptionAtPeriodEndCommand>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentService _paymentService = paymentService;

    public async Task<HandlerResponse> ExecuteAsync(CancelSubscriptionAtPeriodEndCommand command, CancellationToken ct)
    {
        var subscription = await _repository.LoadAsync<Subscription>(command.SubscriptionId.Value, ct);
        if (subscription is null)
        {
            return ("Subscription not found", HandlerResponseStatus.NotFound);
        }

        var succeded = await _paymentService.CancelSubscriptionAtPeriodEndAsync(subscription.GatewaySubscriptionId);
        if (!succeded)
        {
            return ("Failed to cancel subscription at period end", HandlerResponseStatus.InternalError);
        }

        subscription.CancelAtPeriodEnd();

        return HandlerResponseStatus.NoContent;
    }
}
