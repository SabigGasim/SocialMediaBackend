using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.MarkDirectMessageAsSeen;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;

namespace SocialMediaBackend.Modules.Chat.Application.Hubs;

public interface IChatHub
{
    Task UserConnected(Guid userId);
    Task UserDisconnected(Guid userId);
    Task UpdateDirectChatTypingStatus(Guid chatId, bool isTyping);
    Task UpdateGroupChatTypingStatus(Guid chatId, bool isTyping);
    Task NotifyGroupMessageSeen(MarkGroupMessagAsSeenMessage message);
    Task NotifyDirectMessageSeen(MarkDirectMessageAsSeenMessage message);
}
