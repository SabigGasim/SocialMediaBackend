namespace SocialMediaBackend.Domain.Common;
public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken ct = default);
}
