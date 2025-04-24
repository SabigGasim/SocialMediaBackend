using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.ReplyToComment;

public class ReplyToCommentCommand(Guid parentId, Guid userId, string text) : CommandBase<ReplyToCommentResponse>
{
    public CommentId ParentId { get; } = new(parentId);
    public UserId UserId { get; } = new(userId);
    public string Text { get; } = text;
}
