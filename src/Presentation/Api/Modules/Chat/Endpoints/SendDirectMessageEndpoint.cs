using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.CreateDirectMessage;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.SendDirectMessage;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpPost(ApiEndpoints.DirectChat.SendDirectMessage)]
public class SendDirectMessageEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender) 
    : RealtimeEndpoint<SendDirectMessageRequest, SendDirectMessageResponse, DirectMessageMessage, ChatHub>(module, sender)
{
    public override async Task HandleAsync(SendDirectMessageRequest req, CancellationToken ct)
    {
        await HandleSingleUserCommandAsync(new CreateDirectMessageCommand(req.ChatId, req.Text), ct);
    }
}
