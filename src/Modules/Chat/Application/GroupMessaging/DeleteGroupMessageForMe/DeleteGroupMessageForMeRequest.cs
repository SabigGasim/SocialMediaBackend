namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.DeleteGroupMessageForMe;

public record DeleteGroupMessageForMeRequest(Guid ChatId, Guid MessageId);