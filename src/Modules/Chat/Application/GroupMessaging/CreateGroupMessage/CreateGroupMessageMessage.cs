using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.CreateGroupMessage;

public record CreateGroupMessageMessage(
    Guid MessageId, 
    Guid GroupId, 
    Guid SenderId, 
    string Text, 
    DateTimeOffset SentAt) : IRealtimeMessage;
