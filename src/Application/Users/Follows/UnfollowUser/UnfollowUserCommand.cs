using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Users.Follows.UnfollowUser;

public class UnfollowUserCommand(Guid userToUnfollow) : CommandBase, IUserRequest<UnfollowUserCommand>
{
    public Guid UserToUnfollow { get; } = userToUnfollow;

    public Guid UserId { get; private set; }
    public bool IsAdmin { get; private set; }

    public UnfollowUserCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }

    public UnfollowUserCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }
}
