using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;

[HasPermission(Permissions.GetAllDirectMessages)]
public sealed class GetAllDirectMessagesQuery(Guid chatId, int page, int pageSize)
    : QueryBase<GetAllDirectMessagesResponse>, IRequireAuthorization
{
    public DirectChatId DirectChatId { get; } = new(chatId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
