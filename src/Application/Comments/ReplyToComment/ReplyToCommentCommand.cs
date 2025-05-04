using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.ReplyToComment;

public class ReplyToCommentCommand(Guid parentId, string text) 
    : CommandBase<ReplyToCommentResponse>, IUserRequest<ReplyToCommentCommand>
{
    public CommentId ParentId { get; } = new(parentId);
    public string Text { get; } = text;
    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public ReplyToCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public ReplyToCommentCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
