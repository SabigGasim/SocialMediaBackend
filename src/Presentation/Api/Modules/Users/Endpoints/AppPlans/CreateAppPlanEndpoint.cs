using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.AppPlans.CreateAppPlan;
using SocialMediaBackend.Modules.Users.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.AppPlans;

[HttpPost(ApiEndpoints.Payments.CreateAppPlan)]
public class CreateAppPlanEndpoint(IUsersModule module) : RequestEndpoint<CreateAppPlanRequest>(module)
{
    public override Task HandleAsync(CreateAppPlanRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new CreateAppPlanCommand(req.Prices, req.Tier), ct);
    }
}
