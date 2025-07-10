using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.GetAllGroupMessages;

[HasPermission(Permissions.GetAllGroupMessages)]
public sealed class GetAllGroupMessagesQuery(Guid chatId, int page, int pageSize)
    : QueryBase<GetAllGroupMessagesResponse>, IUserRequest
{
    public Guid UserId { get; private set; }
    public bool IsAdmin { get; private set; }
    public GroupChatId GroupChatId { get; } = new(chatId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
