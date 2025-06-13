
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Domain.Common;
using SocialMediaBackend.Modules.Payments.Domain.ValueObjects;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.SubscribeToAppPlan;

public class SubscribeToAppPlanCommandHandler(
    IPaymentService paymentService) : ICommandHandler<SubscribeToAppPlanCommand>
{
    private readonly IPaymentService _paymentService = paymentService;
    private readonly MoneyValue _appPlanPrice = MoneyValue.Create(20_00, Currency.USD);

    public async Task<HandlerResponse> ExecuteAsync(SubscribeToAppPlanCommand command, CancellationToken ct)
    {
        var paymentIntent = await _paymentService.CreatePaymentIntentAsync(_appPlanPrice);
        var paymentMethod = await _paymentService.CreatePaymentMethodAsync();
        var confirmedPaymentIntent = await _paymentService.ConfirmPaymentIntentAsync(paymentIntent.Id, paymentMethod.Id);

        return HandlerResponseStatus.OK;
    }
}
