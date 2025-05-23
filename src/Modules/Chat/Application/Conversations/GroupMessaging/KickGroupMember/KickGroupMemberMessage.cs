using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

public record KickGroupMemberMessage(Guid ChatterId, Guid GroupChatId) : IRealtimeMessage;
