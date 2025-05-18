using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.DeleteDirectMessageForMe;

public class DeleteDirectMessageForMeCommand(Guid messageId, Guid directChatId) : CommandBase, IUserRequest
{
    public Guid UserId { get; private set; }
    public bool IsAdmin {  get; private set; }
    public DirectMessageId MessageId { get; } = new(messageId);
    public DirectChatId DirectChatId { get; } = new(directChatId);

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
