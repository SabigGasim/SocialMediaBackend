using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.LikeComment;
public class LikeCommentCommand(Guid commentId) : CommandBase, IUserRequest<LikeCommentCommand>
{
    public CommentId CommentId { get; } = new(commentId);

    public UserId UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public LikeCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public LikeCommentCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
