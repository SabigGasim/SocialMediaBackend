using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.PromoteMember;

[HasPermission(Permissions.PromoteMember)]
public sealed class PromoteMemberCommand(
    Guid groupChatId, 
    Guid memberId,
    Membership membership)
    : GroupCommandBase<MemberPromotedMessage>, IRequireAuthorization
{
    public GroupChatId GroupChatId { get; } = new(groupChatId);
    public ChatterId MemberId { get; } = new(memberId);
    public Membership Membership { get; } = membership;
}
