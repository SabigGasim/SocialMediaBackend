using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForMe;

[HasPermission(Permissions.DeleteGroupMessageForMe)]
public sealed class DeleteGroupMessageForMeCommand(Guid messageId, Guid groupChatId) : CommandBase, IRequireAuthorization
{
    public GroupMessageId GroupMessageId { get; } = new(messageId);
    public GroupChatId GroupChatId { get; } = new(groupChatId);
}
