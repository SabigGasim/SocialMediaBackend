using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Feed.Posts.Rules;

namespace SocialMediaBackend.Domain.Feed.Posts;

public class Post : AggregateRoot<PostId>
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

    public static Post? Create(AuthorId authorId, string? text = null, IEnumerable<Media>? mediaItems = null)
    {
        CheckRule(new PostShouldHaveTextOrMediaRule(text, mediaItems));

        return new Post(authorId, text, mediaItems);
    }

    public bool UpdatePost(string text)
    {
        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = Id.ToString();

        return true;
    }

    public Comment AddComment(string text, AuthorId authorId)
    {
        var comment = Comment.Create(Id, authorId, text, null);

        _comments.Add(comment);
        CommentsCount++;

        return comment;
    }

    public bool RemoveComment(CommentId commentId)
    {
        var comment = _comments.FirstOrDefault(x => x.Id == commentId);
        if (comment is null)
        {
            return false;
        }

        _comments.Remove(comment);

        AddDomainEvent(new CommentDeletedEvent(commentId));

        return true;
    }

    public PostLike? AddLike(AuthorId authorId)
    {
        if (_likes.Any(l => l.UserId == authorId))
            return null;

        var postLike = PostLike.Create(authorId, Id);
        _likes.Add(postLike);
        LikesCount++;

        return postLike;
    }

    public bool RemoveLike(AuthorId authorId)
    {
        var postLike = _likes.Find(l => l.UserId == authorId);

        if (postLike is null)
            return false;

        _likes.Remove(postLike);
        LikesCount--;

        return true;
    }
}
