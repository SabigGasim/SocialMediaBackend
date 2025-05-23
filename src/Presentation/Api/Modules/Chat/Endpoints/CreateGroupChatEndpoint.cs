using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpPost(ApiEndpoints.GroupChat.CreateGroupChat)]
public class CreateGroupChatEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender) 
    : RealtimeEndpoint<CreateGroupChatRequest, CreateGroupChatResponse, CreateGroupChatMessage, ChatHub>(module, sender)
{
    public override Task HandleAsync(CreateGroupChatRequest req, CancellationToken ct)
    {
        return HandleMultipleUsersCommandAsync(new CreateGroupChatCommand(req.Name, req.Members), ct);
    }
}
