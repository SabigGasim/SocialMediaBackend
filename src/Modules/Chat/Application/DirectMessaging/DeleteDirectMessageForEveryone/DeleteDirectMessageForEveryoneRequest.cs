namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.DeleteDirectMessageForEveryone;

public record DeleteDirectMessageForEveryoneRequest(Guid ChatId, Guid MessageId);
