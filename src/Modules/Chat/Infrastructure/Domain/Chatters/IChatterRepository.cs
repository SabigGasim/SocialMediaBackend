using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;
public interface IChatterRepository
{
    public Task<bool> ExistsAsync(ChatterId chatterId, CancellationToken ct = default);
}
