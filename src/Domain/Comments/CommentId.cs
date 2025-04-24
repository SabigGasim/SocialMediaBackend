using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Comments;

public sealed record CommentId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static CommentId New() => new(Guid.NewGuid());
}
