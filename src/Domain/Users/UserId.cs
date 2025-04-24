using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Users;

public sealed record UserId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static UserId New() => new(Guid.NewGuid());
}
