using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Comments.DeleteComment;

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
