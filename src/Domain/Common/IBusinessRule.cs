namespace SocialMediaBackend.Domain.Common;

public interface IBusinessRule
{
    bool IsBroken();
    Task<bool> IsBrokenAsync();
    string Message { get; }
}
