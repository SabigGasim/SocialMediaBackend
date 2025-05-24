using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.PromoteMember;

public record MemberPromotedMessage(Guid GroupId, Guid MemberId, Membership membership) : IRealtimeMessage;
