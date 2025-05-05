using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Feed.Domain.Posts;

public sealed record PostId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static PostId New() => new(Guid.NewGuid());
}
