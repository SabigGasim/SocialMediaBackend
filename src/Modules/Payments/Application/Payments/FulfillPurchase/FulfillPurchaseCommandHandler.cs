using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.FulfillPurchase;

public class FulfillPurchaseCommandHandler(IAggregateRepository repository)
    : ICommandHandler<FulfillPurchaseCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(FulfillPurchaseCommand command, CancellationToken ct)
    {
        var purchase = await _repository.LoadAsync<Purchase>(command.PurchaseId.Value);

        NotFoundException.ThrowIfNull(nameof(Purchase), purchase);

        purchase.Fulfill(command.PurchasedAt);

        return HandlerResponseStatus.OK;
    }
}
