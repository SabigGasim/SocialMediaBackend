using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Comments.ReplyToComment;

public class ReplyToCommentCommand(Guid parentId, Guid userId, string text) : CommandBase<ReplyToCommentResponse>
{
    public Guid ParentId { get; } = parentId;
    public Guid UserId { get; } = userId;
    public string Text { get; } = text;
}
