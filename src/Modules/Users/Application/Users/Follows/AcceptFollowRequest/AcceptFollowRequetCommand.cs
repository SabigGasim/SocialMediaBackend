using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;
public class AcceptFollowRequetCommand(Guid userToAcceptId) : CommandBase, IUserRequest
{
    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UserId UserToAcceptId { get; } = new(userToAcceptId);

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
