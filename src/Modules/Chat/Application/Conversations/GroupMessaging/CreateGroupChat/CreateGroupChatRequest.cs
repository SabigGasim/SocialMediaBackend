namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

public record CreateGroupChatRequest(string Name, IEnumerable<Guid> Members);
