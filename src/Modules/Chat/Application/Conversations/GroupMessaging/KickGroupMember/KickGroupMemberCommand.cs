using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

[HasPermission(Permissions.KickGroupMember)]
public sealed class KickGroupMemberCommand(Guid chatterId, Guid groupChatId)
    : GroupCommandBase<KickGroupMemberMessage>, IRequireAuthorization

{
    public ChatterId ChatterId { get; } = new(chatterId);
    public GroupChatId GroupChatId { get; } = new(groupChatId);
}
