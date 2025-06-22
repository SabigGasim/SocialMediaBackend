using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Users.Application.AppPlans.SubscribeToAppPlan;
using SocialMediaBackend.Modules.Users.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.AppPlans;

[HttpPost(ApiEndpoints.Payments.SubscriteToAppPlan)]
public class SubscribeToAppPlanEndpoint(IUsersModule module)
    : RequestEndpoint<SubscribeToAppPlanRequest, CreateCheckoutSessionResponse>(module)
{
    public override async Task HandleAsync(SubscribeToAppPlanRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new SubscribeToAppPlanCommand(req.Tier, req.Interval), ct);
    }
}
