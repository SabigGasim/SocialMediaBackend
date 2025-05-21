using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

public interface IChatRepository
{
    Task<IEnumerable<DirectMessageDto>> GetAllDirectChatMessages(
        ChatterId chatterId,
        DirectChatId directChatId,
        int page,
        int pageSize,
        CancellationToken ct = default);
    Task<IEnumerable<string>> GetChattersWithDirectOrGroupChatWith(ChatterId chatterId);
    Task<bool> ExistsAsync(DirectChatId chatId, CancellationToken ct = default);
    Task<bool> ExistsAsync(GroupChatId chatId, CancellationToken ct = default);
    Task<bool> ExistsAsync(UserGroupChatId chatId, CancellationToken ct = default);
    Task<bool> ExistsAsync(UserDirectChatId chatId, CancellationToken ct = default);
    Task<bool> DirectChatExistsAsync(ChatterId firstChatter, ChatterId secondChatter, CancellationToken ct = default);
}
