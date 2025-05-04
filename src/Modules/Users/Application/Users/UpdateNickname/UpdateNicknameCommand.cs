using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

public class UpdateNicknameCommand(string nickname) : CommandBase, IUserRequest<UpdateNicknameCommand>
{
    public string Nickname { get; } = nickname;

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UpdateNicknameCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public UpdateNicknameCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
