using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForEveryone;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpDelete(ApiEndpoints.DirectChat.DeleteMessageForEveryone)]
public class DeleteDirectMessageForEveyroneEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender) 
    : RealtimeEndpoint<DeleteDirectMessageForEveryoneRequest, DeleteDirectMessageMessage, ChatHub>(module, sender)
{
    public override Task HandleAsync(DeleteDirectMessageForEveryoneRequest req, CancellationToken ct)
    {
        return HandleSingleUserCommand(new DeleteDirectMessageForEveryoneCommand(req.ChatId, req.MessageId), ct);
    }
}
