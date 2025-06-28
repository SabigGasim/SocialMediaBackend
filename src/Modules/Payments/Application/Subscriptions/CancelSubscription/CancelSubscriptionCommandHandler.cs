using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;

internal sealed class CancelSubscriptionCommandHandler(IAggregateRepository repository) : ICommandHandler<CancelSubscriptionCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(CancelSubscriptionCommand command, CancellationToken ct)
    {
        var subscription = await _repository.LoadAsync<Subscription>(command.InternalSubscriptionId, ct);

        NotFoundException.ThrowIfNull(nameof(Subscription), subscription);

        subscription.Cancel();

        return HandlerResponseStatus.OK;
    }
}
