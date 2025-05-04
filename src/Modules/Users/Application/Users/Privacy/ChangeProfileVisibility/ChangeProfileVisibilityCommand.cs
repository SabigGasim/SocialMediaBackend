using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;

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
