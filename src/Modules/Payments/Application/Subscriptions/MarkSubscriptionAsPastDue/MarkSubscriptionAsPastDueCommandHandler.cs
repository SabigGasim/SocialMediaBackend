using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsPastDue;

public class MarkSubscriptionAsPastDueCommandHandler(IAggregateRepository repository)
    : ICommandHandler<MarkSubscriptionAsPastDueCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(MarkSubscriptionAsPastDueCommand command, CancellationToken ct)
    {
        var subscription = await _repository.LoadAsync<Subscription>(command.InternalSubscriptionId, ct);

        NotFoundException.ThrowIfNull(nameof(Subscription), subscription);

        subscription.MarkAsPastDue();

        return HandlerResponseStatus.OK;
    }
}
