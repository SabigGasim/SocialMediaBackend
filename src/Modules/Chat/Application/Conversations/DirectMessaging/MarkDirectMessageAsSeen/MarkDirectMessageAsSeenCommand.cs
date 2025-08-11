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
    DirectMessageId directMessageId) : CommandBase, IRequireAuthorization
{
    public DirectChatId DirectChatId { get; } = directChatId;
    public DirectMessageId DirectMessageId { get; } = directMessageId;
}
