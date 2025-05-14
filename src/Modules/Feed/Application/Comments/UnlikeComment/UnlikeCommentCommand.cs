using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.UnlikeComment;
public class UnlikeCommentCommand(Guid commentId) : CommandBase, IUserRequest
{
    public CommentId CommentId { get; } = new(commentId);

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
