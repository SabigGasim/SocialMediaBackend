using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Feed.Posts;

namespace SocialMediaBackend.Application.Posts.UnlikePost;

public class UnlikePostCommand(Guid postId) : CommandBase, IUserRequest<UnlikePostCommand>
{
    public PostId PostId { get; } = new(postId);

    public UserId UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UnlikePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public UnlikePostCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}