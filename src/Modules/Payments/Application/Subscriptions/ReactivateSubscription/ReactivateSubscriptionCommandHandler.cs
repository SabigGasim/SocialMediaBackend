using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.ReactivateSubscription;

internal sealed class ReactivateSubscriptionCommandHandler(
    IAggregateRepository repository,
    IPaymentService paymentService)
    : ICommandHandler<ReactivateSubscriptionCommand>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentService _paymentService = paymentService;

    public async Task<HandlerResponse> ExecuteAsync(ReactivateSubscriptionCommand command, CancellationToken ct)
    {
        var subscription = await _repository.LoadAsync<Subscription>(
            command.SubscriptionId.Value,
            ct);

        if (subscription is null)
        {
            return ("Subscription was not found", HandlerResponseStatus.NotFound);
        }

        var succeded = await _paymentService.ReactivateSubscriptionAsync(subscription.GatewaySubscriptionId);
        if (!succeded)
        {
            return ("Failed to reactivate subscription", HandlerResponseStatus.InternalError);
        }
        
        subscription.Reactivate();

        return HandlerResponseStatus.NoContent;
    }
}
