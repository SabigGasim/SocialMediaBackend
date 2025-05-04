using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

public class UnfollowUserCommand(Guid userToUnfollow) : CommandBase, IUserRequest<UnfollowUserCommand>
{
    public UserId UserToUnfollow { get; } = new(userToUnfollow);

    public Guid UserId { get; private set; } = default!;
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
