using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.AppSubscriptions.Endpoints;

[HttpPost(ApiEndpoints.Payments.CreateAppPlan)]
public class CreateAppPlanEndpoint(IAppSubscriptionsModule module) : RequestEndpoint<CreateAppPlanRequest>(module)
{
    public override Task HandleAsync(CreateAppPlanRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new CreateAppPlanCommand(req.Prices, req.Tier), ct);
    }
}
