namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;

public record SendDirectMessageRequest(string Text, Guid ChatId);
