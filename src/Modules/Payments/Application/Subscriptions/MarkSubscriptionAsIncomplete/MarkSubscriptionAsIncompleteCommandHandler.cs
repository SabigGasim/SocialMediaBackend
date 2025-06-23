using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsIncomplete;

public class MarkSubscriptionAsIncompleteCommandHandler(IAggregateRepository repository)
    : ICommandHandler<MarkSubscriptionAsIncompleteCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(MarkSubscriptionAsIncompleteCommand command, CancellationToken ct)
    {
        var subscription = await _repository.LoadAsync<Subscription>(command.InternalSubscriptionId, ct);

        NotFoundException.ThrowIfNull(nameof(Subscription), subscription);

        subscription.MarkAsIncomplete();

        return HandlerResponseStatus.OK;
    }
}
