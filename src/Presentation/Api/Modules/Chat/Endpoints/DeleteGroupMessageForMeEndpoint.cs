using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.GroupMessaging.DeleteGroupMessageForMe;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpDelete(ApiEndpoints.GroupChat.DeleteMessageForMe)]
public class DeleteGroupMessageForMeEndpoint(IChatModule module) : RequestEndpoint<DeleteGroupMessageForMeRequest>(module)
{
    public override async Task HandleAsync(DeleteGroupMessageForMeRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new DeleteGroupMessageForMeCommand(req.MessageId, req.ChatId), ct);
    }
}
