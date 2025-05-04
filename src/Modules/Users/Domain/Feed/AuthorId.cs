using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Domain.Feed;

public record AuthorId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static AuthorId New() => new(Guid.NewGuid());
}
