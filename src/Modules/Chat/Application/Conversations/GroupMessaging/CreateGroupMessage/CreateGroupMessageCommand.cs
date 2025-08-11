using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;

[HasPermission(Permissions.CreateGroupMessage)]
public sealed class CreateGroupMessageCommand(Guid chatId, string text)
    : GroupCommandBase<CreateGroupMessageMessage, SendGroupMessageResponse>, IRequireAuthorization
{
    public GroupChatId ChatId { get; } = new(chatId);
    public string Text { get; } = text;
}
