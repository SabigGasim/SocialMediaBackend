namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessageAsReceived;

public record MarkGroupMessageAsReceivedRequest(Guid ChatId, Guid MessageId);
