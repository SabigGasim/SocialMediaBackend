using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Posts.UpdatePost;

public class UpdatePostCommand(Guid postId, string text) : CommandBase
{
    public Guid PostId { get; } = postId;
    public string Text { get; } = text;
}
