using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.MarkDirectMessageAsSeen;

[HasPermission(Permissions.MarkDirectMessageAsSeen)]
public sealed class MarkDirectMessageAsSeenCommand(
    DirectChatId directChatId,
    DirectMessageId directMessageId) : CommandBase, IUserRequest
{
    public DirectChatId DirectChatId { get; } = directChatId;

    public DirectMessageId DirectMessageId { get; } = directMessageId;

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
