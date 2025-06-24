using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.RenewSubscription;

public class RenewSubscriptionCommandHandler(IAggregateRepository repository)
    : ICommandHandler<RenewSubscriptionCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(RenewSubscriptionCommand command, CancellationToken ct)
    {
        var subscription = await _repository.LoadAsync<Subscription>(command.InternalSubscriptionId, CancellationToken.None);

        NotFoundException.ThrowIfNull(nameof(Subscription), subscription);

        subscription.Renew(command.StartDate, command.ExpirationDate);

        return HandlerResponseStatus.OK;
    }
}
