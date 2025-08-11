using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;

[HasPermission(Permissions.CreateDirectMessage)]
public sealed class CreateDirectMessageCommand(Guid directChatId, string text)
    : SingleUserCommandBase<DirectMessageMessage, SendDirectMessageResponse>, IRequireAuthorization
{
    public string Text { get; } = text;
    public DirectChatId DirectChatId { get; } = new(directChatId);
}
