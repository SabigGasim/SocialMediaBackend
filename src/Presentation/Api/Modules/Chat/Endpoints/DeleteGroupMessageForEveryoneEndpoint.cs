using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Services;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.GroupMessaging.DeleteGroupMessageForEveryone;
using SocialMediaBackend.Modules.Chat.Application.Hubs;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpDelete(ApiEndpoints.GroupChat.DeleteMessageForEveryone)]
public class DeleteGroupMessageForEveryoneEndpoint(
    IChatModule module,
    IRealtimeMessageSender<ChatHub> sender)
    : RealtimeEndpoint<DeleteGroupMessageForEveryoneRequest, DeleteGroupMessageMessage, ChatHub>(module, sender)
{
    public override async Task HandleAsync(DeleteGroupMessageForEveryoneRequest req, CancellationToken ct)
    {
        await HandleGroupCommandAsync(new DeleteGroupMessageForEveryoneCommand(req.ChatId, req.MessageId), ct);
    }
}
