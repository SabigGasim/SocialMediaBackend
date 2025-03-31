using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Entities;

public class Post : AuditableEntity<Guid>
{
    private readonly List<PostLike> _likes = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Media> _mediaItems = new();

    public Guid UserId { get; private set; }
    public int LikesCount { get; private set; }
    public int CommentsCount { get; private set; }
    public string? Text { get; private set; }

    public User User { get; private set; } = default!;
    public IReadOnlyCollection<PostLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Media> MediaItems => _mediaItems.AsReadOnly();
}
