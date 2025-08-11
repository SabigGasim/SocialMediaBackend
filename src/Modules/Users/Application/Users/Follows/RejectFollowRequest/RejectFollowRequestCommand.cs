using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequest;

[HasPermission(Permissions.RejectFollowRequests)]
public sealed class RejectFollowRequestCommand(Guid userToRejectId) : CommandBase, IRequireAuthorization
{
    public UserId UserToRejectId { get; } = new(userToRejectId);
}
