using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppSubscriptionProduct;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.AppSubscriptions.Endpoints;

[HttpPost(ApiEndpoints.Payments.CreateAppSubscriptionProduct)]
public class CreateAppSubscriptionProductEndpoint(IAppSubscriptionsModule module)
    : RequestEndpoint<CreateAppSubscriptionRequest>(module)
{
    public override async Task HandleAsync(CreateAppSubscriptionRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new CreateAppSubscriptionProductCommand(req.Tier), ct);
    }
}
