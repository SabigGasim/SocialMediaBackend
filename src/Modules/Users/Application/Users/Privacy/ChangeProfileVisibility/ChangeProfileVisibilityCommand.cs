using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Users.Application.Users.Privacy.ChangeProfileVisibility;

public class ChangeProfileVisibilityCommand(bool isPublic) : CommandBase, IUserRequest
{
    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }
    public bool IsPublic { get; } = isPublic;

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
