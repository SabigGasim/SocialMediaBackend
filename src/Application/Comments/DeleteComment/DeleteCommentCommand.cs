using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.DeleteComment;

public class DeleteCommentCommand(Guid commentId, Guid postId) : CommandBase, IUserRequest<DeleteCommentCommand>
{
    public CommentId CommentId { get; } = new(commentId);
    public PostId PostId { get; } = new(postId);

    public UserId UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public DeleteCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public DeleteCommentCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
