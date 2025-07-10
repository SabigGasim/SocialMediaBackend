using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Feed.Domain.Authors;

public sealed record AuthorId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static AuthorId New() => new(Guid.NewGuid());
}
