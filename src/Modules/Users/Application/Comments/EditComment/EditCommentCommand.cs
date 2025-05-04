using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;

namespace SocialMediaBackend.Modules.Users.Application.Comments.EditComment;

public class EditCommentCommand(Guid commentId, string text) : CommandBase, IUserRequest<EditCommentCommand>
{
    public CommentId CommentId { get; } = new(commentId);
    public string Text { get; } = text;

    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public EditCommentCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public EditCommentCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
