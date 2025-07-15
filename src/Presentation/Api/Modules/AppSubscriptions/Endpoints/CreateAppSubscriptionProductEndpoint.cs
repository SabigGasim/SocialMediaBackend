using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppSubscriptionProduct;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.AppSubscriptions.Endpoints;

public class CreateAppSubscriptionProductEndpoint(IAppSubscriptionsModule module)
    : RequestEndpoint<CreateAppSubscriptionRequest>(module)
{
    public override void Configure()
    {
        Post(ApiEndpoints.Payments.CreateAppSubscriptionProduct);
        Description(x => x.Accepts<CreateAppSubscriptionRequest>());
    }

    public override async Task HandleAsync(CreateAppSubscriptionRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new CreateAppSubscriptionProductCommand(req.Tier), ct);
    }
}
