using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

[HasPermission(Permissions.FollowUsers)]
public sealed class FollowUserCommand(Guid userToFollowId) : CommandBase<FollowUserResponse>, IUserRequest
{
    public UserId UserToFollowId { get; } = new(userToFollowId);

    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
