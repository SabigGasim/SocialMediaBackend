using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.DomainServices.Users;

public class UserExistsChecker : IUserExistsChecker
{
    private readonly IUserRepository _userRepository;

    public UserExistsChecker(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<bool> CheckAsync(Guid userId, CancellationToken token = default)
    {
        return _userRepository.ExistsAsync(userId, token);
    }

    public Task<bool> CheckAsync(string username, CancellationToken token = default)
    {
        return _userRepository.ExistsAsync(username, token);
    }
}
