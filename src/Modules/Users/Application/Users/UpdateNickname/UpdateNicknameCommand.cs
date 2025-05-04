using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;

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
