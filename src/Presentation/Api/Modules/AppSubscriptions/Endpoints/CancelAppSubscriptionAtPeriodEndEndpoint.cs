using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.CancelSubscriptionAtPeriodEnd;

namespace SocialMediaBackend.Api.Modules.AppSubscriptions.Endpoints;

public class CancelAppSubscriptionAtPeriodEndEndpoint(IAppSubscriptionsModule module) : RequestEndpoint(module)
{
    public override void Configure()
    {
        Delete(ApiEndpoints.Payments.CancelSubscriptionAtPeriodEnd);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await HandleCommandAsync(new CancelSubscriptionAtPeriodEndCommand(), ct);
    }
}
