using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.DeleteDirectMessageForEveryone;

public record DeleteDirectMessageMessage(Guid MessageId) : IRealtimeMessage;
