namespace SocialMediaBackend.Modules.Chat.Application.Hubs;

public interface IChatHub
{
    Task UserConnected(Guid userId);
    Task UserDisconnected(Guid userId);
    Task UpdateDirectChatTypingStatus(Guid chatId, bool isTyping);
}
