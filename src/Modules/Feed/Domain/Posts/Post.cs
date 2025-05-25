using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts.Rules;

namespace SocialMediaBackend.Modules.Feed.Domain.Posts;

public class Post : AggregateRoot<PostId>, IUserResource
{
    private readonly List<PostLike> _likes = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Media> _mediaItems;

    private Post(AuthorId authorId, string? text,
        IEnumerable<Media>? mediaItems = null)
    {
        AuthorId = authorId;
        Text = text;

        Id = PostId.New();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = new DateTimeOffset(Created.DateTime, Created.Offset);
        LastModifiedBy = "System";

        _mediaItems = new(mediaItems ?? []);
    }

    private Post() => _mediaItems = new();

    public AuthorId AuthorId { get; private set; } = default!;
    public Author Author { get; private set; } = default!;
    public int LikesCount { get; private set; }
    public int CommentsCount { get; private set; }
    public string? Text { get; private set; }

    public IReadOnlyCollection<PostLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Media> MediaItems => _mediaItems.AsReadOnly();

    public static Result<Post> Create(AuthorId authorId, string? text = null, IEnumerable<Media>? mediaItems = null)
    {
        CheckRule(new PostShouldHaveTextOrMediaRule(text, mediaItems));

        return new Post(authorId, text, mediaItems);
    }

    public Result UpdatePost(string text)
    {
        CheckRule(new PostShouldHaveTextOrMediaRule(text, _mediaItems));

        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = Id.ToString();

        return Result.Success();
    }

    public Comment AddComment(string text, AuthorId authorId)
    {
        var comment = Comment.Create(Id, authorId, text, null);

        _comments.Add(comment);
        CommentsCount++;

        return comment;
    }

    public Result RemoveComment(CommentId commentId)
    {
        var comment = _comments.FirstOrDefault(x => x.Id == commentId);
        if (comment is null)
        {
            return Result.Failure(FailureCode.NotFound, "Comment");
        }

        _comments.Remove(comment);

        this.AddDomainEvent(new CommentDeletedEvent(commentId));

        return Result.Success();
    }

    public Result<PostLike> AddLike(AuthorId authorId)
    {
        if (_likes.Any(l => l.UserId == authorId))
        {
            return Result<PostLike>.FailureWithMessage(FailureCode.Duplicate, "Post is already liked");
        }

        var like = PostLike.Create(authorId, Id);
        
        _likes.Add(like);
        LikesCount++;

        return like;
    }

    public Result RemoveLike(AuthorId authorId)
    {
        var like = _likes.Find(l => l.UserId == authorId);
        if (like is null)
        {
            return Result.FailureWithMessage(FailureCode.NotFound, "Post is not liked");
        }

        _likes.Remove(like);
        LikesCount--;

        return Result.Success();
    }
}
