using SocialMediaBackend.Application.Abstractions.Requests.Commands;

namespace SocialMediaBackend.Application.Posts.DeletePost;

public class DeletePostCommand(Guid postId) : CommandBase
{
    public Guid PostId { get; } = postId;
}
