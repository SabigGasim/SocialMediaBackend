using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;

[HasPermission(Permissions.MarkGroupMessageAsSeen)]
public class MarkGroupMessageAsSeenCommand(Guid groupId) : CommandBase<GroupMessageId?>, IRequireAuthorization
{
    public GroupChatId GroupChatId { get; } = new(groupId);
}
