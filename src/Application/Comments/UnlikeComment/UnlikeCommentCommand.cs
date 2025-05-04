using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.UnlikeComment;
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
