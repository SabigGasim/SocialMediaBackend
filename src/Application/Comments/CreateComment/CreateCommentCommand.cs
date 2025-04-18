using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Comments.CreateComment;

public class CreateCommentCommand(Guid userId, Guid postId, string text) : CommandBase<CreateCommentResponse>
{
    public Guid UserId { get; } = userId;
    public Guid PostId { get; } = postId;
    public string Text { get; } = text;
}
