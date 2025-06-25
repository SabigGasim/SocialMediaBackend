using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;
using SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.CreatePaymentCheckoutSession;

public class CreatePaymentCheckoutSessionCommandHandler(
    IAggregateRepository repository,
    IPaymentGateway paymentGateway)
    : ICommandHandler<CreatePaymentCheckoutSessionCommand, CreateCheckoutSessionResponse>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentGateway _paymentGateway = paymentGateway;

    public async Task<HandlerResponse<CreateCheckoutSessionResponse>> ExecuteAsync(CreatePaymentCheckoutSessionCommand command, CancellationToken ct)
    {
        var payerTask = _repository.LoadAsync<Payer>(command.PayerId.Value, ct);
        var productTask = _repository.LoadAsync<Product>(x => x.Reference == command.ProductReference, ct);

        await Task.WhenAll(payerTask, productTask);

        var payer = await payerTask;
        var product = await productTask;

        NotFoundException.ThrowIfNull(payer, product);

        var (_, gatewayPriceId, price) = product.Prices.First(x => x.Id == command.PriceId);

        if (price.PaymentInterval != PaymentInterval.OneTime)
        {
            return ("Can't purchase a product with recurring payment interval", HandlerResponseStatus.Conflict);
        }

        var purchase = Purchase.Initiate(command.PayerId, command.ProductReference);

        _repository.StartStream(purchase);

        return await _paymentGateway.CreateOneTimePaymentCheckoutSessionAsync(
            payer.GatewayCustomerId,
            product.Reference,
            gatewayPriceId,
            command.SuccessUrl,
            command.CancelUrl,
            purchase.Id,
            ct);
    }
}