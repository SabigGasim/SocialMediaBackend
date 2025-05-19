using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.DeleteDirectMessageForEveryone;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpDelete(ApiEndpoints.DirectChat.DeleteMessageForEveryone)]
public class DeleteDirectMessageForEveyroneEndpoint(IChatModule module) 
    : RequestEndpoint<DeleteDirectMessageForEveryoneRequest>(module)
{
    public override Task HandleAsync(DeleteDirectMessageForEveryoneRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeleteDirectMessageForEveryoneCommand(req.ChatId, req.MessageId), ct);
    }
}
