using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.SendDirectMessage;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpPost(ApiEndpoints.DirectChat.SendDirectMessage)]
public class SendDirectMessageEndpiont(IChatModule module) : RequestEndpoint<SendDirectMessageRequest, SendDirectMessageResponse>(module)
{
    public override async Task HandleAsync(SendDirectMessageRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new CreateDirectMessageCommand(req.ChatId, req.Text), ct);
    }
}
