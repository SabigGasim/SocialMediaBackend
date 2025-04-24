using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Posts;

namespace SocialMediaBackend.Application.Posts.DeletePost;

public class DeletePostCommand(Guid postId) : CommandBase
{
    public PostId PostId { get; } = new(postId);
}
