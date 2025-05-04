using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Posts.DeletePost;

public class DeletePostCommand(Guid postId) : CommandBase, IUserRequest<DeletePostCommand>
{
    public PostId PostId { get; } = new(postId);

    public Guid UserId { get; private set; } = default!;

    public bool IsAdmin { get; private set; }

    public DeletePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public DeletePostCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
