using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Feed;

public record AuthorId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static AuthorId New() => new(Guid.NewGuid());
}
