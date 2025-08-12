using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure;
using SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.UpgradeSubscription;

internal sealed class UpgradeSubscriptionCommandHandler(
    IPaymentService paymentService,
    IPaymentGateway paymentGateway,
    IAggregateRepository repository)
    : ICommandHandler<UpgradeSubscriptionCommand, CreateCheckoutSessionResponse>
{
    private readonly IPaymentService _paymentService = paymentService;
    private readonly IPaymentGateway _paymentGateway = paymentGateway;
    private readonly IAggregateRepository _repository = repository;

    public async Task<HandlerResponse<CreateCheckoutSessionResponse>> ExecuteAsync(UpgradeSubscriptionCommand command, CancellationToken ct)
    {
        var subscrpition = await _repository.LoadAsync<Subscription>(command.SubscriptionId.Value, ct);
        var product = await _repository.LoadAsync<Product>(x => x.Reference == subscrpition.ProductReference, ct);

        var gatewayPriceId = product
            .Prices
            .First(x => x.Id == command.PriceId)
            .GatewayPriceId;

        var updatedSuscription = await _paymentService.UpgradeSubscriptionPlanAsync(
            subscrpition.GatewaySubscriptionId, 
            gatewayPriceId);

        if (updatedSuscription.Status == "active")
        {
            return CreateCheckoutSessionResponse.CreateCompleted();
        }

        return await _paymentGateway.CreateBillingPortalSessionForSubscriptionUpgradeAsync(
            updatedSuscription.CustomerId,
            subscrpition.GatewaySubscriptionId,
            gatewayPriceId);
    }
}
