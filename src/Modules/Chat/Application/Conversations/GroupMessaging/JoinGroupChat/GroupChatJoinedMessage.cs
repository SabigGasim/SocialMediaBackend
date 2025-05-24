using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;

public record GroupChatJoinedMessage(Guid GroupId, Guid MemberId) : IRealtimeMessage;
