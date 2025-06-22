using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Products.CreateProduct;

public class CreateProductCommandHandler(
    IPaymentService paymentService,
    IAggregateRepository repository)
    : ICommandHandler<CreateProductCommand, ProductId>
{
    private readonly IPaymentService _paymentService = paymentService;
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse<ProductId>> ExecuteAsync(CreateProductCommand command, CancellationToken ct)
    {
        var gatewayProductId = await _paymentService.CreateProductAsync(
            command.ProductReference, 
            command.Name, 
            command.Description
            );

        var product = Product.Create(
            command.ProductReference,
            gatewayProductId,
            command.Owner
            );

        _repository.StartStream(product);

        return new ProductId(product.Id);
    }
}
