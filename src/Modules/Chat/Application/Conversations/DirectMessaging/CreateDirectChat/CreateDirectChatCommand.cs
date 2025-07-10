using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;

[HasPermission(Permissions.CreateDirectChat)]
public sealed class CreateDirectChatCommand(Guid otherChatterId)
    : CommandBase<CreateDirectChatResponse>, IUserRequest
{
    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }
    public ChatterId OtherChatterId { get; } = new(otherChatterId);

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
