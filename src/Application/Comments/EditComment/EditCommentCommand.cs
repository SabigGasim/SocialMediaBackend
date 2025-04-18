using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Comments.EditComment;

public class EditCommentCommand(Guid commentId, string text) : CommandBase
{
    public Guid CommentId { get; } = commentId;
    public string Text { get; } = text;
}
