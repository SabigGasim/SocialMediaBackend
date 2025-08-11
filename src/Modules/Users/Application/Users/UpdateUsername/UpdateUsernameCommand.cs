using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateUsername;

[HasPermission(Permissions.ModifyUserInfo)]
public sealed class UpdateUsernameCommand(string username) : CommandBase, IRequireAuthorization
{
    public string Username { get; } = username;
}
