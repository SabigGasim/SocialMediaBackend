using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Posts.UnlikePost;

public class UnlikePostCommand(Guid postId) : CommandBase, IUserRequest<UnlikePostCommand>
{
    public PostId PostId { get; } = new(postId);

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UnlikePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public UnlikePostCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}