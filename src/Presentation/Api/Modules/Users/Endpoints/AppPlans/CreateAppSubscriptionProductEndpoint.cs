using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.AppPlans.CreateAppSubscriptionProduct;
using SocialMediaBackend.Modules.Users.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.AppPlans;

[HttpPost(ApiEndpoints.Payments.CreateAppSubscriptionProduct)]
public class CreateAppSubscriptionProductEndpoint(IUsersModule module)
    : RequestEndpoint<CreateAppSubscriptionRequest>(module)
{
    public override async Task HandleAsync(CreateAppSubscriptionRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new CreateAppSubscriptionProductCommand(req.Tier), ct);
    }
}
