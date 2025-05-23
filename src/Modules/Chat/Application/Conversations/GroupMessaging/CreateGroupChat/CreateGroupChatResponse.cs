namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

public record CreateGroupChatResponse(Guid Id, IEnumerable<Guid>? UnaddedMembers);