using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Payments.Application.Webhooks.StripeWebhookEventReceived;

public class StripeWebhookEventReceivedCommand(Stripe.Event @event) : CommandBase
{
    public Stripe.Event Event { get; } = @event;
}