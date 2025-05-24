using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.PromoteMember;

public class PromoteMemberCommand(
    Guid groupChatId, 
    Guid memberId,
    Membership membership)
    : GroupCommandBase<MemberPromotedMessage>, IUserRequest
{
    public GroupChatId GroupChatId { get; } = new(groupChatId);
    public ChatterId MemberId { get; } = new(memberId);
    public Membership Membership { get; } = membership;
    
    public Guid UserId { get; private set; }
    public bool IsAdmin {  get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
