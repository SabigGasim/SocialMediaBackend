using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;

public class CreateGroupMessageCommand(Guid chatId, string text)
    : GroupCommandBase<CreateGroupMessageMessage, SendGroupMessageResponse>, IUserRequest
{
    public GroupChatId ChatId { get; } = new(chatId);
    public string Text { get; } = text;

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
