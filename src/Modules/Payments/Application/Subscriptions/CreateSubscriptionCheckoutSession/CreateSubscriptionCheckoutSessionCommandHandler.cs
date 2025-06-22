using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CreateSubscriptionCheckoutSession;

public class CreateSubscriptionCheckoutSessionCommandHandler(
    IAggregateRepository repository,
    IPaymentGateway paymentGateway)
    : ICommandHandler<CreateSubscriptionCheckoutSessionCommand, CreateCheckoutSessionResponse>
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IPaymentGateway _paymentGateway = paymentGateway;

    public async Task<HandlerResponse<CreateCheckoutSessionResponse>> ExecuteAsync(CreateSubscriptionCheckoutSessionCommand command, CancellationToken ct)
    {
        var payer = await _repository.LoadAsync<Payer>(command.PayerId.Value, ct);
        var product = await _repository.LoadAsync<Product>(x => x.Reference == command.ProductReference, ct);

        NotFoundException.ThrowIfNull(payer, product);

        var subscription = Subscription.Initiate(command.PayerId, command.ProductReference);

        _repository.StartStream(subscription);

        var (_, gatwayPriceId, _) = product.Prices.First(x => x.Id == command.PriceId);

        return await _paymentGateway.CreateSubscriptionCheckoutSessionAsync(
            payer.GatewayCustomerId,
            product.Reference,
            gatwayPriceId,
            command.SuccessUrl,
            command.CancelUrl,
            subscription.Id,
            ct);
    }
}
