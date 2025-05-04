using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.Follows.AcceptFollowRequest;
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
