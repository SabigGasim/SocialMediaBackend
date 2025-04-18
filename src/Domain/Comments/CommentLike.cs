﻿using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Comments;


public record CommentLike : ValueObject
{
    public Guid UserId { get; private set; }
    public Guid CommentId { get; private set; }
    public DateTimeOffset Created { get; private set; }

    private CommentLike(Guid userId, Guid commentId)
    {
        UserId = userId;
        CommentId = commentId;
        Created = DateTimeOffset.UtcNow;
    }

    private CommentLike() { }

    internal static CommentLike Create(Guid userId, Guid commentId)
    {
        return new CommentLike(userId, commentId);
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return CommentId;
        yield return Created;
    }
}