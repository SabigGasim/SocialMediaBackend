using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Products.CreateProductPrice;

internal sealed class CreateProductPriceCommandHandler(
    IAggregateRepository repository,
    IPaymentService paymentService)
    : ICommandHandler<CreateProductPriceCommand, ProductPriceId>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentService _paymentService = paymentService;

    public async Task<HandlerResponse<ProductPriceId>> ExecuteAsync(CreateProductPriceCommand command, CancellationToken ct)
    {
        var product = await _repository.LoadAsync<Product>(command.ProductId, ct);

        NotFoundException.ThrowIfNull(nameof(product), product);

        var gatewayPriceId = await _paymentService.CreatePriceAsync(
            product.GatewayProductId,
            command.ProductPrice
            );

        var priceId = product.AddPrice(gatewayPriceId, command.ProductPrice);

        return priceId;
    }
}
