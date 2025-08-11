using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.Privacy.ChangeProfileVisibility;

[HasPermission(Permissions.ModifyUserInfo)]
public sealed class ChangeProfileVisibilityCommand(bool isPublic) : CommandBase, IRequireAuthorization
{
    public bool IsPublic { get; } = isPublic;
}
