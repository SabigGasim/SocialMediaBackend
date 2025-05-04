using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequet;
public class RejectFollowRequestCommand(Guid userToRejectId) : CommandBase, 
    IUserRequest<RejectFollowRequestCommand>
{
    public UserId UserToRejectId { get; } = new(userToRejectId);

    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public RejectFollowRequestCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public RejectFollowRequestCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
