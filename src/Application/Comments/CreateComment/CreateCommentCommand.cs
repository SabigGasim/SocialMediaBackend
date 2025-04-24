using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.CreateComment;

public class CreateCommentCommand(Guid userId, Guid postId, string text) : CommandBase<CreateCommentResponse>
{
    public UserId UserId { get; } = new(userId);
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;
}
