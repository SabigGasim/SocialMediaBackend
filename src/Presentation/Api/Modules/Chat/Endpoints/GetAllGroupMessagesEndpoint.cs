using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.GroupMessaging.GetAllGroupMessages;

namespace SocialMediaBackend.Api.Modules.Chat.Endpoints;

[HttpGet(ApiEndpoints.GroupChat.GetAllMessages)]
public class GetAllGroupMessagesEndpoint(IChatModule module)
    : RequestEndpoint<GetAllGroupMessagesRequest, GetAllGroupMessagesResponse>(module)
{
    public override async Task HandleAsync(GetAllGroupMessagesRequest req, CancellationToken ct)
    {
        await HandleQueryAsync(new GetAllGroupMessagesQuery(req.ChatId, req.Page, req.PageSize), ct);
    }
}
