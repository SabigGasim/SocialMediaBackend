using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

[HasPermission(Permissions.CreateGroupChat)]
public sealed class CreateGroupChatCommand(string groupName, IEnumerable<Guid> memberIds)
    : MultipleUsersCommandBase<CreateGroupChatMessage, CreateGroupChatResponse>, IUserRequest
{
    public string GroupName { get; } = groupName;
    public IEnumerable<Guid> MemberIds { get; } = memberIds;

    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
