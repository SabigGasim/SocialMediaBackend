namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public interface IHubConnectionTracker
{
    Task<IEnumerable<string>> GetConnectionsAsync(string userId);
    Task AddConnectionAsync(string userId, string connectionId);
    Task RemoveConnectionAsync(string userId, string connectionId);
}
