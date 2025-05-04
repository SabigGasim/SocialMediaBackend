using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Feed.Posts;

public record PostLike : ValueObject, IUserResource
{
    public UserId UserId { get; private set; } = default!;
    public PostId PostId { get; private set; } = default!;
    public DateTimeOffset Created { get; private set; }
    public User User { get; private set; } = default!;
    public Post Post { get; private set; } = default!;

    private PostLike(UserId userId, PostId postId)
    {
        UserId = userId;
        PostId = postId;
        Created = DateTimeOffset.UtcNow;
    }

    private PostLike() { }

    internal static PostLike Create(UserId userId, PostId postId)
    {
        return new PostLike(userId, postId);
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return PostId;
        yield return Created;
    }
}