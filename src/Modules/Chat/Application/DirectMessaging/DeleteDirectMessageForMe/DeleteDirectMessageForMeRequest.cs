namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.DeleteDirectMessageForMe;

public record DeleteDirectMessageForMeRequest(Guid ChatId, Guid MessageId);
