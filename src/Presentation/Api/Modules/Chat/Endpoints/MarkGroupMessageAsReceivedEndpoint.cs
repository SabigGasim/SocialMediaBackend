using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessageAsReceived;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

public class MarkGroupMessageAsReceivedEndpoint(IChatModule module)
    : RequestEndpoint<MarkGroupMessageAsReceivedRequest>(module)
{
    public override async Task HandleAsync(MarkGroupMessageAsReceivedRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new MarkGroupMessageAsReceivedCommand(req.ChatId, req.MessageId), ct);
    }
}
