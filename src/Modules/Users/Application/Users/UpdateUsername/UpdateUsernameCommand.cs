using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateUsername;

public class UpdateUsernameCommand(string username) : CommandBase, IUserRequest<UpdateUsernameCommand>
{
    public string Username { get; } = username;

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UpdateUsernameCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public UpdateUsernameCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
