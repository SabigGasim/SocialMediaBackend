using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

[HasPermission(Permissions.DeleteUsers)]
public sealed class DeleteUserCommand(Guid userToDeleteId) : CommandBase, IRequireAuthorization
{
    public UserId UserToDeleteId { get; } = new(userToDeleteId);
}
