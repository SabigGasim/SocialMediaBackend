using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.GetAllGroupMessages;

[HasPermission(Permissions.GetAllGroupMessages)]
public sealed class GetAllGroupMessagesQuery(Guid chatId, int page, int pageSize)
    : QueryBase<GetAllGroupMessagesResponse>, IRequireAuthorization
{
    public GroupChatId GroupChatId { get; } = new(chatId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
