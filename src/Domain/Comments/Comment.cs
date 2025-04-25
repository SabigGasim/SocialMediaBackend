using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Comments;

public class Comment : AggregateRoot<CommentId>, IUserResource
{
    private readonly List<CommentLike> _likes = new();
    private readonly List<Comment> _replies = new();

    private Comment(PostId postId, UserId userId,
        string text, CommentId? parentCommentId = null)
    {
        PostId = postId;
        UserId = userId;
        Text = text;
        ParentCommentId = parentCommentId;

        Id = CommentId.New();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = userId.ToString();
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = userId.ToString();
    }

    private Comment() { }

    public PostId PostId { get; private set; } = default!;
    public Post Post { get; private set; } = default!;
    public UserId UserId { get; private set; } = default!;
    public User User { get; private set; } = default!;
    public CommentId? ParentCommentId { get; private set; }
    public Comment? ParentComment { get; private set; }
    public string Text { get; private set; } = default!;
    public int LikesCount { get; private set; }
    public int RepliesCount { get; private set; }

    public IReadOnlyCollection<CommentLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

    public static Comment Create(PostId postId, UserId userId, string text, CommentId? parentCommentId)
    {
        return new Comment(postId, userId, text, parentCommentId);
    }

    public Comment AddReply(PostId postId, UserId userId, string text)
    {
        var reply = Create(postId, userId, text, parentCommentId: this.Id);

        _replies.Add(reply);
        RepliesCount++;

        return reply;
    }

    public CommentLike? AddLike(UserId userId)
    {
        if (_likes.Any(x => x.UserId == userId))
        {
            return null;
        }

        var like = CommentLike.Create(userId, Id);
        _likes.Add(like);
        LikesCount++;

        return like;
    }

    public bool RemoveLike(UserId userId)
    {
        var like = _likes.Find(x => x.UserId == userId);
        if (like is null)
            return false;

        _likes.Remove(like);
        LikesCount--;

        return true;
    }

    public bool RemoveReply(CommentId replyId)
    {
        var reply = _replies.Find(x => x.Id == replyId)!;
        _replies.Remove(reply);
        RepliesCount--;

        return true;
    }

    public bool Edit(string text)
    {
        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = this.Id.ToString();

        return true;
    }
}