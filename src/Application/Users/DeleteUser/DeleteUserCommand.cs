using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.DeleteUser;

public class DeleteUserCommand(Guid userToDeleteId) : CommandBase, IUserRequest<DeleteUserCommand>
{
    public Guid UserId { get; private set; } = default!;
    public UserId UserToDeleteId { get; } = new(userToDeleteId);

    public bool IsAdmin { get; private set; }

    public DeleteUserCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public DeleteUserCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
