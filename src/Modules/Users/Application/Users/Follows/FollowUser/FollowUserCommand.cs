using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Authorization;
using SocialMediaBackend.BuildingBlocks.Application.Auth;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

[HasPermission(Permissions.FollowUsers)]
public sealed class FollowUserCommand(Guid userToFollowId) : CommandBase<FollowUserResponse>, IRequireAuthorization
{
    public UserId UserToFollowId { get; } = new(userToFollowId);
}
