using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.DirectMessaging.DeleteDirectMessageForMe;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpDelete(ApiEndpoints.DirectChat.DeleteMessageForMe)]
public class DeleteDirectMessageForMeEndpoint(IChatModule module) : RequestEndpoint<DeleteDirectMessageForMeRequest>(module)
{
    public override async Task HandleAsync(DeleteDirectMessageForMeRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new DeleteDirectMessageForMeCommand(req.MessageId, req.ChatId), ct);
    }
}
