using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;

[HasPermission(Permissions.LeaveGroupChat)]
public sealed class LeaveGroupChatCommand(Guid groupChatId)
    : GroupCommandBase<GroupChatLeftMessage>, IRequireAuthorization
{
    public GroupChatId GroupChatId { get; } = new(groupChatId);
}
