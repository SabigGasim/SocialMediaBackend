using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;

public record MarkGroupMessagAsSeenMessage(Guid GroupId, Guid MessageId, Guid MemberId) : IRealtimeMessage;
