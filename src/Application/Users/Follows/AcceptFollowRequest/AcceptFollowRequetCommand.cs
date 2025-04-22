using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Users.Follows.AcceptFollowRequest;
public class AcceptFollowRequetCommand(Guid userToAcceptId) : CommandBase, IUserRequest<AcceptFollowRequetCommand>
{
    public Guid UserId { get; private set; }
    public bool IsAdmin { get; private set; }

    public Guid UserToAcceptId { get; } = userToAcceptId;

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
