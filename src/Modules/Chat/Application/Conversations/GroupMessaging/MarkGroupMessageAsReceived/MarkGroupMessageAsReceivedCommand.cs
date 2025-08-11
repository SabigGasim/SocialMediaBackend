using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessageAsReceived;

[HasPermission(Permissions.MarkGroupMessageAsReceived)]
public sealed class MarkGroupMessageAsReceivedCommand(Guid groupChatId, Guid messageId) : CommandBase, IRequireAuthorization
{
    public GroupChatId GroupChatId { get; } = new(groupChatId);
    public GroupMessageId MessageId { get; } = new(messageId);
}
