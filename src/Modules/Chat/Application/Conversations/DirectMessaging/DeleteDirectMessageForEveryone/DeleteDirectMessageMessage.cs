using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForEveryone;

public record DeleteDirectMessageMessage(Guid MessageId) : IRealtimeMessage;
