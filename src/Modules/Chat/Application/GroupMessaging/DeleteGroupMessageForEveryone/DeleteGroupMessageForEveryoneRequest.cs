namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.DeleteGroupMessageForEveryone;

public record DeleteGroupMessageForEveryoneRequest(Guid ChatId, Guid MessageId);
