using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Domain.Feed.Comments;

public sealed record CommentId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static CommentId New() => new(Guid.NewGuid());
}
