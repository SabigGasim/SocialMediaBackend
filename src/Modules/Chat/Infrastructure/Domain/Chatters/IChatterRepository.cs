using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;
public interface IChatterRepository
{
    Task<bool> ExistsAsync(ChatterId chatterId, CancellationToken ct = default);
    Task SetOnlineStatus(ChatterId chatterId, bool status);
    Task<ChatterDto?> GetByIdAsync(ChatterId chatterId, CancellationToken ct = default);
}
