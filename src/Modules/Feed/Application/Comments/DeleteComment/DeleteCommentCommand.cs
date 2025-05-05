using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;

public class DeleteCommentCommand(Guid commentId, Guid postId) : CommandBase, IUserRequest<DeleteCommentCommand>
{
    public CommentId CommentId { get; } = new(commentId);
    public PostId PostId { get; } = new(postId);

    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public DeleteCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public DeleteCommentCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
