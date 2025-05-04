using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Posts.DeletePost;

public class DeletePostCommand(Guid postId) : CommandBase, IUserRequest<DeletePostCommand>
{
    public PostId PostId { get; } = new(postId);

    public UserId UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public DeletePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public DeletePostCommand WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
