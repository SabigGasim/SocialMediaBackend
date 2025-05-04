using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Domain.Feed.Comments;

public sealed record CommentId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static CommentId New() => new(Guid.NewGuid());
}
