using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

public sealed record PostId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static PostId New() => new(Guid.NewGuid());
}
