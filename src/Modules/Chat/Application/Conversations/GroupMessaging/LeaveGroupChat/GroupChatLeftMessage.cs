using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;

public record GroupChatLeftMessage(Guid GroupId, Guid MemberId) : IRealtimeMessage;
