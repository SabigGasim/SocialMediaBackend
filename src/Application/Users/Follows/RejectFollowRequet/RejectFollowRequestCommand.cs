using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Users.Follows.RejectFollowRequet;
public class RejectFollowRequestCommand(Guid userToRejectId) : CommandBase, 
    IUserRequest<RejectFollowRequestCommand>
{
    public Guid UserToRejectId { get; } = userToRejectId;

    public Guid UserId { get; private set; }

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
