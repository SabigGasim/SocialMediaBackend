﻿using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppPlan;

internal sealed class CreateAppPlanCommandHandler(
    IPaymentAntiCorruptionLayer paymentAntiCorruptionLayer,
    SubscriptionsDbContext context) : ICommandHandler<CreateAppPlanCommand>
{
    private readonly IPaymentAntiCorruptionLayer _paymentAntiCorruptionLayer = paymentAntiCorruptionLayer;
    private readonly SubscriptionsDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(CreateAppPlanCommand command, CancellationToken ct)
    {
        var product = await _context.AppSubscriptionProducts
            .Where(x => x.Tier == command.Tier)
            .Include(x => x.Plans)
            .FirstOrDefaultAsync(ct);

        if (product is null)
        {
            return ($"App subscription product with tier '{command.Tier}' was not found.", HandlerResponseStatus.NotFound);
        }

        var priceTasks = command.Prices.Select(async price => new
        {
            Value = price,
            Id = await _paymentAntiCorruptionLayer.CreatePriceAsync(product.Id.Value, price, ct)
        });

        await foreach(var priceTask in Task.WhenEach(priceTasks))
        {
            var price = await priceTask;

            var appPlan = product.AddPlan(new AppSubscriptionPlanId(price.Id), price.Value);

            _context.Add(appPlan);
        }

        return HandlerResponseStatus.Created;
    }
}
