using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;
using SocialMediaBackend.Modules.Users.Domain.Users;
namespace SocialMediaBackend.Modules.Users.Application.Posts.LikePost;

public class LikePostCommand(Guid postId) : CommandBase, IUserRequest<LikePostCommand>
{
    public PostId PostId { get; } = new(postId);

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public LikePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public LikePostCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
