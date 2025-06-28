using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.ProcessPaymentCreated;

internal sealed class ProcessPaymentCreatedCommandHandler(IAggregateRepository repository)
    : ICommandHandler<ProcessPaymentCreatedCommand>
{
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse> ExecuteAsync(ProcessPaymentCreatedCommand command, CancellationToken ct)
    {
        var purchase = await _repository.LoadAsync<Purchase>(command.PurchaseId.Value);

        NotFoundException.ThrowIfNull(nameof(Purchase), purchase);

        purchase.MarkCreated(command.PaymentGatewayId);

        return HandlerResponseStatus.OK;
    }
}
