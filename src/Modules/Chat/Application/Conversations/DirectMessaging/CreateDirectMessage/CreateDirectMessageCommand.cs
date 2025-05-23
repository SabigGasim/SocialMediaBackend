using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;
public class CreateDirectMessageCommand(Guid directChatId, string text)
    : SingleUserCommandBase<DirectMessageMessage, SendDirectMessageResponse>, IUserRequest
{
    public string Text { get; } = text;
    public DirectChatId DirectChatId { get; } = new(directChatId);
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
