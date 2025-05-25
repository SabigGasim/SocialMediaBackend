using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Domain.Comments;

public class Comment : AggregateRoot<CommentId>, IUserResource
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

    public Result<CommentLike> AddLike(AuthorId userId)
    {
        if (_likes.Any(x => x.UserId == userId))
        {
            return Result<CommentLike>.FailureWithMessage(FailureCode.Duplicate, "Comment is already liked");
        }

        var like = CommentLike.Create(userId, Id);
        _likes.Add(like);
        LikesCount++;

        return like;
    }

    public Result RemoveLike(AuthorId userId)
    {
        var like = _likes.Find(x => x.UserId == userId);
        if (like is null)
        {
            return Result.FailureWithMessage(FailureCode.NotFound, "This comment is not liked");
        }

        _likes.Remove(like);
        LikesCount--;

        return Result.Success();
    }

    public Result RemoveReply(CommentId replyId)
    {
        var reply = _replies.Find(x => x.Id == replyId);
        if (reply is null)
        {
            return Result.Failure(FailureCode.NotFound, "Reply");
        }

        _replies.Remove(reply);
        RepliesCount--;

        return Result.Success();
    }

    public void Edit(string text)
    {
        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = this.Id.ToString();
    }
}