namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForMe;

public record DeleteDirectMessageForMeRequest(Guid ChatId, Guid MessageId);
