using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.Follows.UnfollowUser;

public class UnfollowUserCommand(Guid userToUnfollow) : CommandBase, IUserRequest<UnfollowUserCommand>
{
    public UserId UserToUnfollow { get; } = new(userToUnfollow);

    public UserId UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UnfollowUserCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }

    public UnfollowUserCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }
}
