using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForMe;

[HasPermission(Permissions.DeleteGroupMessageForMe)]
public sealed class DeleteGroupMessageForMeCommand(Guid messageId, Guid groupChatId) : CommandBase, IUserRequest
{
    public GroupMessageId GroupMessageId { get; } = new(messageId);
    public GroupChatId GroupChatId { get; } = new(groupChatId);

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
