using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Payments.Application.Stripe.WebhookEventReceived;

public class StripeWebhookEventReceivedCommand : CommandBase
{
    public string EventType { get; set; } = default!;
    public string EventId { get; set; } = default!;
    public string Data { get; set; } = default!;
    public string Signature { get; set; } = default!;
    public string WebHookSecret { get; set; } = default!;
}
