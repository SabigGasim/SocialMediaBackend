using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.CreateDirectMessage;

public record DirectMessageMessage(Guid MessageId, Guid ChatId, string Message, DateTimeOffset SentAt) : IRealtimeMessage;
