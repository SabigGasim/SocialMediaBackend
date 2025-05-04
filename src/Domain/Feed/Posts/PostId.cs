using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Feed.Posts;

public sealed record PostId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static PostId New() => new(Guid.NewGuid());
}
