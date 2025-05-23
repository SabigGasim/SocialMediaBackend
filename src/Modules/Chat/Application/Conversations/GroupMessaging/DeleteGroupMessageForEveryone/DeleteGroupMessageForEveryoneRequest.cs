namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForEveryone;

public record DeleteGroupMessageForEveryoneRequest(Guid ChatId, Guid MessageId);
