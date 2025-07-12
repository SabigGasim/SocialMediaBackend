namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Users;

public interface IUserRepository
{
    Task<bool> ExistsAsync(Guid userId, CancellationToken token = default);
}
