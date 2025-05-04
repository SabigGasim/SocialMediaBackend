using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Users.Domain.Services;

namespace SocialMediaBackend.Modules.Users.Domain.Users.Rules;

internal class UsernameShouldBeUniqueRule : IBusinessRule
{
    private readonly IUserExistsChecker _userExistsChecker;
    private readonly string _username;

    public UsernameShouldBeUniqueRule(IUserExistsChecker userExistsChecker, string username)
    {
        _userExistsChecker = userExistsChecker;
        _username = username;
        
        Message = $"A user with this username ({username}) already exists!";
    }

    public bool IsBroken() => _userExistsChecker.CheckAsync(_username).Result;

    public Task<bool> IsBrokenAsync(CancellationToken ct = default) => _userExistsChecker.CheckAsync(_username);

    public string Message { get; }
}
