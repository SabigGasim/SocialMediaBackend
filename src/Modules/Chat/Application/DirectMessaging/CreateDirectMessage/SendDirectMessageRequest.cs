namespace SocialMediaBackend.Modules.Chat.Application.DirectMessaging.SendDirectMessage;

public record SendDirectMessageRequest(string Text, Guid ChatId);
