using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpPost(ApiEndpoints.GroupChat.SendMessage)]
public class SendGroupMessageEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender) 
    : RealtimeEndpoint<SendGroupMessageRequest, SendGroupMessageResponse, CreateGroupMessageMessage, ChatHub>(module, sender)
{
    public override Task HandleAsync(SendGroupMessageRequest req, CancellationToken ct)
    {
        return HandleMultipleUsersCommandAsync(new CreateGroupMessageCommand(req.GroupId, req.Text), ct);
    }
}
