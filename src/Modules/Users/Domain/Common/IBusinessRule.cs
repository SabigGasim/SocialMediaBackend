namespace SocialMediaBackend.Modules.Users.Domain.Common;

public interface IBusinessRule
{
    bool IsBroken();
    Task<bool> IsBrokenAsync(CancellationToken ct = default);
    string Message { get; }
}
