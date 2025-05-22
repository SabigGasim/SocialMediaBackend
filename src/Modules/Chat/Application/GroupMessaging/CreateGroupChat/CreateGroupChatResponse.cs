namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.CreateGroupChat;

public record CreateGroupChatResponse(Guid Id, IEnumerable<Guid>? UnaddedMembers);