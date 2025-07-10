using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Authorization;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

[HasPermission(Permissions.ModifyUserInfo)]
public sealed class UpdateNicknameCommand(string nickname) : CommandBase, IUserRequest
{
    public string Nickname { get; } = nickname;

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
