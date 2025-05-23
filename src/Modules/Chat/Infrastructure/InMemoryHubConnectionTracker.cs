using SocialMediaBackend.BuildingBlocks.Infrastructure;
using System.Collections.Concurrent;

namespace SocialMediaBackend.Modules.Chat.Infrastructure;

public class InMemoryHubConnectionTracker : IHubConnectionTracker
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, byte>> _connections = new();

    public Task AddConnectionAsync(string userId, string connectionId)
    {
        var userConnections = _connections.GetOrAdd(userId, _ => new ConcurrentDictionary<string, byte>());
        userConnections.TryAdd(connectionId, 0);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<string>> GetConnectionsAsync(string userId)
    {
        if (_connections.TryGetValue(userId, out var userConnections))
        {
            return Task.FromResult(userConnections.Keys.AsEnumerable());
        }

        return Task.FromResult(Enumerable.Empty<string>());
    }

    public Task RemoveConnectionAsync(string userId, string connectionId)
    {
        if (_connections.TryGetValue(userId, out var userConnections))
        {
            userConnections.TryRemove(connectionId, out _);

            if (userConnections.IsEmpty)
            {
                _connections.TryRemove(userId, out _);
            }
        }

        return Task.CompletedTask;
    }
}

