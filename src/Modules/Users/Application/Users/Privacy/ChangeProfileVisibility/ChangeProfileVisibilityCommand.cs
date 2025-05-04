using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Users.Application.Users.Privacy.ChangeProfileVisibility;

public class ChangeProfileVisibilityCommand(bool isPublic) : CommandBase, IUserRequest<ChangeProfileVisibilityCommand>
{
    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }
    public bool IsPublic { get; } = isPublic;

    public ChangeProfileVisibilityCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public ChangeProfileVisibilityCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
