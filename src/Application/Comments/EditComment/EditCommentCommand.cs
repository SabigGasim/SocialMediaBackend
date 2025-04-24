using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Comments;

namespace SocialMediaBackend.Application.Comments.EditComment;

public class EditCommentCommand(Guid commentId, string text) : CommandBase
{
    public CommentId CommentId { get; } = new(commentId);
    public string Text { get; } = text;
}
