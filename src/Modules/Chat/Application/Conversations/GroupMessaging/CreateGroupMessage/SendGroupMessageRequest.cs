namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;

public record SendGroupMessageRequest(Guid ChatId, string Text);
