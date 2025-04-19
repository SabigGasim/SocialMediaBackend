using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Posts.Rules;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Posts;

public class Post : AuditableEntity<Guid>
{
    private readonly List<PostLike> _likes = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Media> _mediaItems;

    private Post(Guid userId, string? text,
        IEnumerable<Media>? mediaItems = null)
    {
        UserId = userId;
        Text = text;

        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = new DateTimeOffset(Created.DateTime, Created.Offset);
        LastModifiedBy = "System";

        _mediaItems = new(mediaItems ?? []);
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

    public static Post? Create(Guid userId, string? text = null, IEnumerable<Media>? mediaItems = null)
    {
        CheckRule(new PostShouldHaveTextOrMediaRule(text, mediaItems));

        return new Post(userId, text, mediaItems);
    }

    public bool UpdatePost(string text)
    {
        Text = text;
        LastModified = TimeProvider.System.GetUtcNow();
        LastModifiedBy = this.Id.ToString();
        
        return true;
    }

    public Comment AddComment(string text, Guid userId)
    {
        var comment = Comment.Create(this.Id, userId, text, null);

        _comments.Add(comment);
        CommentsCount++;

        return comment;
    }

    public bool RemoveComment(Guid commentId)
    {
        var comment = _comments.First(x => x.Id == commentId);

        _comments.Remove(comment);
        
        return comment.ParentComment is not null
            ? comment.ParentComment.RemoveReply(comment)
            : true;
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
}
