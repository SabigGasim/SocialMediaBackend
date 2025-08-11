using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

[HasPermission(Permissions.UnfollowUsers)]
public sealed class UnfollowUserCommand(Guid userToUnfollow) : CommandBase, IRequireAuthorization
{
    public UserId UserToUnfollow { get; } = new(userToUnfollow);
}
