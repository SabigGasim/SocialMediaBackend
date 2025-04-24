using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.Privacy.ChangeProfileVisibility;

public class ChangeProfileVisibilityCommand(bool isPublic) : CommandBase, IUserRequest<ChangeProfileVisibilityCommand>
{
    public UserId UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }
    public bool IsPublic { get; } = isPublic;

    public ChangeProfileVisibilityCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public ChangeProfileVisibilityCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
