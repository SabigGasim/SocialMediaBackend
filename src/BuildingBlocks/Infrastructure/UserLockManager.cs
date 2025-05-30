using Medallion.Threading;
using Medallion.Threading.Redis;
using StackExchange.Redis;
using System.Runtime.CompilerServices;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public class UserLockManager : IUserLockMangaer
{
    private readonly RedisDistributedSynchronizationProvider _provider;
    private readonly TimeSpan _defaultTimeOut = TimeSpan.FromHours(10);

    public UserLockManager(IConnectionMultiplexer redis)
    {
        _provider = new RedisDistributedSynchronizationProvider(redis.GetDatabase());
    }
    public async Task<IDistributedSynchronizationHandle> AcquireLockAsync(string userId, [CallerMemberName] string? method = null, TimeSpan? lockTimeout = null)
    {
        var key = $"lock:method{method}:user:{userId}";
        var timeout = lockTimeout ?? _defaultTimeOut;

        return await _provider.AcquireLockAsync(key, timeout);
    }
}
