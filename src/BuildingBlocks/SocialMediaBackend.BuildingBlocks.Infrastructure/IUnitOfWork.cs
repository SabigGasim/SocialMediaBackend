namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken ct = default);
}
