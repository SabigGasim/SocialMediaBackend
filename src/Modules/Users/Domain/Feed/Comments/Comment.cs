using SocialMediaBackend.Modules.Users.Domain.Common;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.Domain.Feed.Comments;

public class Comment : AggregateRoot<CommentId>
{
    private readonly List<CommentLike> _likes = new();
    private readonly List<Comment> _replies = new();

    private Comment(PostId postId, AuthorId authorId,
        string text, CommentId? parentCommentId = null)
    {
        PostId = postId;
        AuthorId = authorId;
        Text = text;
        ParentCommentId = parentCommentId;

        Id = CommentId.New();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = authorId.ToString();
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = authorId.ToString();
    }

    private Comment() { }

    public PostId PostId { get; private set; } = default!;
    public Post Post { get; private set; } = default!;
    public AuthorId AuthorId { get; private set; } = default!;
    public Author Author { get; private set; } = default!;
    public CommentId? ParentCommentId { get; private set; }
    public Comment? ParentComment { get; private set; }
    public string Text { get; private set; } = default!;
    public int LikesCount { get; private set; }
    public int RepliesCount { get; private set; }

    public IReadOnlyCollection<CommentLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

    public static Comment Create(PostId postId, AuthorId authorId, string text, CommentId? parentCommentId)
    {
        return new Comment(postId, authorId, text, parentCommentId);
    }

    public Comment AddReply(AuthorId authorId, string text)
    {
        var reply = Create(PostId, authorId, text, parentCommentId: Id);

        _replies.Add(reply);
        RepliesCount++;

        return reply;
    }

    public CommentLike? AddLike(AuthorId userId)
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

    public bool RemoveLike(AuthorId userId)
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
        var reply = _replies.Find(x => x.Id == replyId);
        if (reply is null)
        {
            return false;
        }

        _replies.Remove(reply);
        RepliesCount--;

        return true;
    }

    public bool Edit(string text)
    {
        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = Id.ToString();

        return true;
    }
}