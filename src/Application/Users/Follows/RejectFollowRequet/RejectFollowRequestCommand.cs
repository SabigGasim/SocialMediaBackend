using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Users.Follows.RejectFollowRequet;
public class RejectFollowRequestCommand(Guid userToRejectId) : CommandBase, 
    IUserRequest<RejectFollowRequestCommand>
{
    public UserId UserToRejectId { get; } = new(userToRejectId);

    public UserId UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public RejectFollowRequestCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public RejectFollowRequestCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
