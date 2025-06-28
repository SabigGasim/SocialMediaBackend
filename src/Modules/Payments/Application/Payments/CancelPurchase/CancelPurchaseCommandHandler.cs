using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.CancelPurchase;

internal sealed class CancelPurchaseCommandHandler(IAggregateRepository repository)
    : ICommandHandler<CancelPurchaseCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(CancelPurchaseCommand command, CancellationToken ct)
    {
        var purchase = await _repository.LoadAsync<Purchase>(command.PurchaseId.Value);

        NotFoundException.ThrowIfNull(nameof(Purchase), purchase);

        purchase.Cancel();

        return HandlerResponseStatus.OK;
    }
}
