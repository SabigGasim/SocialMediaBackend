using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

public class DeleteUserCommand(Guid userToDeleteId) : CommandBase, IUserRequest
{
    public Guid UserId { get; private set; } = default!;
    public UserId UserToDeleteId { get; } = new(userToDeleteId);

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
