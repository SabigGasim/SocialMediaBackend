using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Authorization;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;

[HasPermission(Permissions.AcceptFollowRequests)]
public sealed class AcceptFollowRequestCommand(Guid userToAcceptId) : CommandBase, IRequireAuthorization
{
    public UserId UserToAcceptId { get; } = new(userToAcceptId);
}
