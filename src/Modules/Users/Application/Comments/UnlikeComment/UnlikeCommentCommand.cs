using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;

namespace SocialMediaBackend.Modules.Users.Application.Comments.UnlikeComment;
public class UnlikeCommentCommand(Guid commentId) : CommandBase, IUserRequest<UnlikeCommentCommand>
{
    public CommentId CommentId { get; } = new(commentId);

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UnlikeCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public UnlikeCommentCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
