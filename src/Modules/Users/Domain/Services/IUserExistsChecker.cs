namespace SocialMediaBackend.Modules.Users.Domain.Services;

public interface IUserExistsChecker
{
    Task<bool> CheckAsync(Guid userId, CancellationToken token = default);
    Task<bool> CheckAsync(string username, CancellationToken token = default);
}
