using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.RefundPayment;

internal sealed class RefundPaymentCommandHandler(IAggregateRepository repository)
    : ICommandHandler<RefundPaymentCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(RefundPaymentCommand command, CancellationToken ct)
    {
        var purchase = await _repository.LoadAsync<Purchase>(command.PurchaseId.Value);

        NotFoundException.ThrowIfNull(nameof(Purchase), purchase);

        purchase.Refund(command.RefundedAt);

        return HandlerResponseStatus.OK;
    }
}
