using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

public sealed record UserId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static UserId New() => new(Guid.NewGuid());
}
