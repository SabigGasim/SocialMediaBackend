using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpGet(ApiEndpoints.DirectChat.GetAllMessages)]
public class GetAllDirectMessagesEndpoint(IChatModule module)
    : RequestEndpoint<GetAllDirectMessagesRequest, GetAllDirectMessagesResponse>(module)
{
    public override Task HandleAsync(GetAllDirectMessagesRequest req, CancellationToken ct)
    {
        return HandleQueryAsync(new GetAllDirectMessagesQuery(req.ChatId, req.Page, req.PageSize), ct);
    }
}
