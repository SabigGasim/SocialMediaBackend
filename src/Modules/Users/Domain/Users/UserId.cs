using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Domain.Users;

public sealed record UserId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static UserId New() => new(Guid.NewGuid());
}
