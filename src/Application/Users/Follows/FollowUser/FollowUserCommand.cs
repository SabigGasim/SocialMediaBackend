using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Users.Follows.FollowUser;

public class FollowUserCommand(Guid userToFollowId) : CommandBase<FollowUserResponse>,
    IUserRequest<FollowUserCommand>
{
    public Guid UserToFollowId { get; } = userToFollowId;

    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }

    public FollowUserCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }

    public FollowUserCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }
}
