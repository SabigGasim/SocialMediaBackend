namespace SocialMediaBackend.BuildingBlocks.Domain;

public interface IBusinessRule
{
    bool IsBroken();
    Task<bool> IsBrokenAsync(CancellationToken ct = default);
    string Message { get; }
}
