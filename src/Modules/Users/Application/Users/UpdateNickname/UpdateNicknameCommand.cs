using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

[HasPermission(Permissions.ModifyUserInfo)]
public sealed class UpdateNicknameCommand(string nickname) : CommandBase, IRequireAuthorization
{
    public string Nickname { get; } = nickname;
}
