using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;
public class AcceptFollowRequetCommand(Guid userToAcceptId) : CommandBase, IUserRequest<AcceptFollowRequetCommand>
{
    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UserId UserToAcceptId { get; } = new(userToAcceptId);

    public AcceptFollowRequetCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public AcceptFollowRequetCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
