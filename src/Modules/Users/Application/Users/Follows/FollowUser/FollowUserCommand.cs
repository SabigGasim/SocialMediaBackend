using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

public class FollowUserCommand(Guid userToFollowId) : CommandBase<FollowUserResponse>,
    IUserRequest<FollowUserCommand>
{
    public UserId UserToFollowId { get; } = new(userToFollowId);

    public Guid UserId { get; private set; } = default!;

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
