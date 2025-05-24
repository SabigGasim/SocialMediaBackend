using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.PromoteMember;

public record PromoteMemberRequest(Guid ChatId, Guid MemberId, Membership Membership);
