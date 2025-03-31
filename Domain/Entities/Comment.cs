using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Entities;

public class Comment : Entity<Guid>
{
    private readonly List<CommentLike> _likes = new();
    private readonly List<Comment> _replies = new();
    public string? Text { get; private set; }
    public Guid PostId { get; private set; }
    public Post Post { get; private set; } = default!;
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;
    public Guid? ParentCommentId { get; private set; }
    public Comment? ParentComment { get; private set; }
    public int LikesCount { get; private set; }
    public int RepilesCount { get; private set; }

    public IReadOnlyCollection<CommentLike> Likes => _likes.AsReadOnly();
    public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

}