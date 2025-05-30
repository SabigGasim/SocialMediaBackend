using Medallion.Threading;
using System.Runtime.CompilerServices;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public interface IUserLockMangaer
{
    Task<IDistributedSynchronizationHandle> AcquireLockAsync(string userId, [CallerMemberName] string? method = null, TimeSpan? lockTimeout = null);
}