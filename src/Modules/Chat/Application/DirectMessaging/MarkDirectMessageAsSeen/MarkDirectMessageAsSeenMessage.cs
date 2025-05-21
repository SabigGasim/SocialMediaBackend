using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.MarkDirectMessageAsSeen;

public record MarkDirectMessageAsSeenMessage(Guid DirectChatId, Guid LastSeenMessagId) : IRealtimeMessage;
