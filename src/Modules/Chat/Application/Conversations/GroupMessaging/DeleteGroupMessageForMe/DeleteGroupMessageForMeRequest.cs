namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForMe;

public record DeleteGroupMessageForMeRequest(Guid ChatId, Guid MessageId);