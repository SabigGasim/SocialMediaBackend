using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.SubscribeToAppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.AppSubscriptions.Endpoints;

[HttpPost(ApiEndpoints.Payments.SubscriteToAppPlan)]
public class SubscribeToAppPlanEndpoint(IAppSubscriptionsModule module)
    : RequestEndpoint<SubscribeToAppPlanRequest, CreateCheckoutSessionResponse>(module)
{
    public override async Task HandleAsync(SubscribeToAppPlanRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new SubscribeToAppPlanCommand(req.Tier, req.Interval), ct);
    }
}
