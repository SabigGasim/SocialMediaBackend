using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.SubscribeToAppPlan;

namespace SocialMediaBackend.Api.Modules.Payments.Endpoints;

public class SubscribeToAppPlanEndpoint(IPaymentsModule module) : RequestEndpoint(module)
{
    public override void Configure()
    {
        Post(ApiEndpoints.Payments.SubscriteToAppPlan);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await HandleCommandAsync(new SubscribeToAppPlanCommand(), ct);
    }
}
