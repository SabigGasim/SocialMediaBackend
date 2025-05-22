namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.CreateGroupChat;

public record CreateGroupChatRequest(string Name, IEnumerable<Guid> Members);
