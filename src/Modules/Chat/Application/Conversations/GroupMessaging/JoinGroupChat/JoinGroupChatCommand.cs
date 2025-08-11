using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;

[HasPermission(Permissions.JoinGroupChat)]
public sealed class JoinGroupChatCommand(Guid groupChatId)
    : GroupCommandBase<GroupChatJoinedMessage>, IRequireAuthorization
{
    public GroupChatId GroupChatId { get; } = new(groupChatId);
}
