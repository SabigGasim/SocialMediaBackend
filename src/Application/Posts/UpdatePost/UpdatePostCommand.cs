using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Posts;

namespace SocialMediaBackend.Application.Posts.UpdatePost;

public class UpdatePostCommand(Guid postId, string text) : CommandBase
{
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;
}
