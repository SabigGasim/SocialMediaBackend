using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Posts;

public class Post : AuditableEntity<Guid>
{
    private readonly List<PostLike> _likes = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Media> _mediaItems;

    private Post(Guid userId, string? text,
        List<Media>? mediaItems = null)
    {
        _mediaItems = mediaItems ?? new();
        UserId = userId;
        Text = text;

        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = "System";
    }

    private Post() => _mediaItems = new();

    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;
    public int LikesCount { get; private set; }
    public int CommentsCount { get; private set; }
    public string? Text { get; private set; }

    public IReadOnlyCollection<PostLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Media> MediaItems => _mediaItems.AsReadOnly();

    public static Post? Create(Guid userId, string? text = null, List<Media>? mediaItems = null)
    {
        if (string.IsNullOrEmpty(text) && mediaItems == null)
        {
            return null;
        }

        return new Post(userId, text, mediaItems);
    }

    public bool AddComment(string text, Guid userId, Guid? parentCommentId = null)
    {
        if (!_comments.Any(c => c.ParentCommentId == parentCommentId))
            return false;

        var comment = Comment.Create(userId, Id, text, parentCommentId);
        if (comment is null)
            return false;

        _comments.Add(comment);
        CommentsCount++;

        return true;
    }

    public bool RemoveComment(Guid commentId)
    {
        var comment = _comments.Find(c => c.Id == commentId);
        if (comment is null)
            return false;

        _comments.Remove(comment);
        CommentsCount--;

        return true;
    }

    public bool AddLike(Guid userId)
    {
        if (_likes.Any(l => l.UserId == userId))
            return false;

        var postLike = PostLike.Create(userId, Id);
        _likes.Add(postLike);
        LikesCount++;

        return true;
    }

    public bool RemoveLike(Guid userId)
    {
        var postLike = _likes.Find(l => l.UserId == userId);
        if (postLike is null)
            return false;

        _likes.Remove(postLike);
        LikesCount--;

        return true;
    }

    public bool LikeComment(Guid userId, Guid commentId)
    {
        var comment = _comments.Find(c => c.Id == commentId);
        if (comment is null)
            return false;

        return comment.AddLike(userId);
    }

    public bool UnlikeComment(Guid userId, Guid commentId)
    {
        var comment = _comments.Find(c => c.Id == commentId);
        if (comment is null)
            return false;

        return comment.RemoveLike(userId);
    }
}
