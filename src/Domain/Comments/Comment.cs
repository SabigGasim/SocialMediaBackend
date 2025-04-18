using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Comments;

public class Comment : AuditableEntity<Guid>
{
    private readonly List<CommentLike> _likes = new();
    private readonly List<Comment> _replies = new();

    private Comment(Guid postId, Guid userId,
        string text, Guid? parentCommentId = null)
    {
        PostId = postId;
        UserId = userId;
        Text = text;
        ParentCommentId = parentCommentId;

        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = userId.ToString();
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = userId.ToString();
    }

    private Comment() { }

    public Guid PostId { get; private set; }
    public Post Post { get; private set; } = default!;
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;
    public Guid? ParentCommentId { get; private set; }
    public Comment? ParentComment { get; private set; }
    public string Text { get; private set; }
    public int LikesCount { get; private set; }
    public int RepliesCount { get; private set; }

    public IReadOnlyCollection<CommentLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

    internal static Comment? Create(Guid postId, Guid userId, string text, Guid? parentCommentId)
    {
        if (string.IsNullOrEmpty(text))
            return null;

        return new Comment(postId, userId, text, parentCommentId);
    }

    internal bool AddReply(Guid postId, Guid userId, string text)
    {
        var comment = Create(postId, userId, text, this.Id);
        if (comment is null)
            return false;

        _replies.Add(comment);
        RepliesCount++;

        return true;
    }

    internal bool AddLike(Guid userId)
    {
        var like = CommentLike.Create(userId, Id);
        _likes.Add(like);
        LikesCount++;

        return true;
    }

    internal bool RemoveLike(Guid userId)
    {
        var like = _likes.Find(x => x.UserId == userId);
        if (like is null)
            return false;

        _likes.Remove(like);
        LikesCount--;

        return true;
    }

    internal bool RemoveReply(Comment comment)
    {
        _replies.Remove(comment);
        RepliesCount--;

        return true;
    }

    internal bool Edit(string text)
    {
        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = this.Id.ToString();

        return true;
    }
}