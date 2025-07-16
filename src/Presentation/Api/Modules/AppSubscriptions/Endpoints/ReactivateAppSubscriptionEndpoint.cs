using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.ReactivateSubscription;

namespace SocialMediaBackend.Api.Modules.AppSubscriptions.Endpoints;

[HttpPost(ApiEndpoints.Payments.ReactivateAppSubscription)]
public class ReactivateAppSubscriptionEndpoint(IAppSubscriptionsModule module) : RequestEndpoint(module)
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        await HandleCommandAsync(new ReactivateSubscriptionCommand(), ct);
    }
}
