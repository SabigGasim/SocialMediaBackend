namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

public record KickGroupMemberRequest(Guid MemberId, Guid ChatId);
