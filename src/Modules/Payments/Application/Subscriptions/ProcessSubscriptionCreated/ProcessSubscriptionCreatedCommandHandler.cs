using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.ProcessSubscriptionCreated;

internal sealed class ProcessSubscriptionCreatedCommandHandler(IAggregateRepository repository)
    : ICommandHandler<ProcessSubscriptionCreatedCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(ProcessSubscriptionCreatedCommand command, CancellationToken ct)
    {
        var subscription = await _repository.LoadAsync<Subscription>(command.InternalSubscriptionId, CancellationToken.None);

        NotFoundException.ThrowIfNull(nameof(Subscription), subscription);

        subscription.MarkCreated(command.GatewaySubscriptionId);

        return HandlerResponseStatus.OK;
    }
}
