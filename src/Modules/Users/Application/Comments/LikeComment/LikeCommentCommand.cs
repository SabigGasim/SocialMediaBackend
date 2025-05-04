using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;

namespace SocialMediaBackend.Modules.Users.Application.Comments.LikeComment;
public class LikeCommentCommand(Guid commentId) : CommandBase, IUserRequest<LikeCommentCommand>
{
    public CommentId CommentId { get; } = new(commentId);

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public LikeCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public LikeCommentCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
