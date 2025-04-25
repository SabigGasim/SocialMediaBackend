using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;
namespace SocialMediaBackend.Application.Posts.LikePost;

public class LikePostCommand(Guid postId) : CommandBase, IUserRequest<LikePostCommand>
{
    public PostId PostId { get; } = new(postId);

    public UserId UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public LikePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public LikePostCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
