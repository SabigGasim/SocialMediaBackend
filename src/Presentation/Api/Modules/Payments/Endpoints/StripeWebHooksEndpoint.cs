using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Stripe.WebhookEventReceived;
using SocialMediaBackend.Modules.Payments.Infrastructure;
using Stripe;
using System.Buffers;
using System.Net;
using System.Text;

namespace SocialMediaBackend.Api.Modules.Payments.Endpoints;

[HttpPost(ApiEndpoints.Payments.StripeWebHooks)]
[AllowAnonymous]
public class StripeWebHooksEndpoint : RequestEndpoint
{
    private readonly StripeSettings _stripeSettings;
    private readonly IPaymentsModule _module;

    public StripeWebHooksEndpoint(IOptions<StripeSettings> stripeSettings, IPaymentsModule module) 
        : base(module)
    {
        _stripeSettings = stripeSettings.Value;
        _module = module;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        string jsonString;
        using (var reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8))
        {
            jsonString = await reader.ReadToEndAsync(ct);
        }

        try
        {
            string signature = HttpContext.Request.Headers["Stripe-Signature"]!;

            var stripeEvent = EventUtility.ConstructEvent(
                jsonString,
                signature,
                _stripeSettings.WebHookSecret
            );

            var command = new StripeWebhookEventReceivedCommand
            {
                EventType = stripeEvent.Type,
                EventId = stripeEvent.Id,
                Data = jsonString,
                Signature = signature,
                WebHookSecret = _stripeSettings.WebHookSecret
            };

            await _module.ExecuteCommandAsync(command, ct);

            await SendOkAsync(ct);
        }
        catch (StripeException e)
        {
            AddError(e.Message, HttpStatusCode.BadRequest.ToString());
            await SendErrorsAsync((int)HttpStatusCode.BadRequest, cancellation: ct);
        }
    }

}
