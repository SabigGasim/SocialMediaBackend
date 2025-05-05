using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.UnlikeComment;
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
