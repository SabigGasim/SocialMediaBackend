using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForMe;

[HasPermission(Permissions.DeleteDirectMessageForMe)]
public sealed class DeleteDirectMessageForMeCommand(Guid messageId, Guid directChatId) : CommandBase, IRequireAuthorization
{
    public DirectMessageId MessageId { get; } = new(messageId);
    public DirectChatId DirectChatId { get; } = new(directChatId);
}
