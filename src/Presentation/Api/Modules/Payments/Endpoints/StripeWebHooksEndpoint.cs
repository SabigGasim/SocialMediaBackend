using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Webhooks.StripeWebhookEventReceived;
using SocialMediaBackend.Modules.Payments.Infrastructure;
using Stripe;
using System.Text;

namespace SocialMediaBackend.Api.Modules.Payments.Endpoints;

[HttpPost(ApiEndpoints.Payments.StripeWebHooks)]
[AllowAnonymous]
public class StripeWebHooksEndpoint(IOptions<StripeSettings> stripeSettings, IPaymentsModule module) : RequestEndpoint(module)
{
    private readonly StripeSettings _stripeSettings = stripeSettings.Value;

    public override async Task HandleAsync(CancellationToken ct)
    {
        string jsonString;
        using (var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
        {
            jsonString = await reader.ReadToEndAsync(ct);
        }

        string signature = HttpContext.Request.Headers["Stripe-Signature"]!;

        Event stripeEvent = EventUtility.ConstructEvent(
            jsonString,
            signature,
            _stripeSettings.WebHookSecret
        );

        var command = new StripeWebhookEventReceivedCommand(stripeEvent);

        await HandleCommandAsync(command, ct);
    }

}
