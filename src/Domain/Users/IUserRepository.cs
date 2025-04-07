namespace SocialMediaBackend.Domain.Users;

public interface IUserRepository
{
    Task<bool> ExistsAsync(Guid userId, CancellationToken token = default);
    Task<bool> ExistsAsync(string username, CancellationToken token = default);
}
