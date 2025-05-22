namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.CreateGroupMessage;

public record SendGroupMessageRequest(Guid GroupId, string Text);
