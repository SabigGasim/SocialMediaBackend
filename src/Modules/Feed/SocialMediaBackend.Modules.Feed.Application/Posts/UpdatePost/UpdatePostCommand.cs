using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UpdatePost;

public class UpdatePostCommand(Guid postId, string text) : CommandBase, IUserRequest<UpdatePostCommand>
{
    public PostId PostId { get; } = new(postId);
    public string Text { get; } = text;

    public Guid UserId { get; private set; } = default!;
    public bool IsAdmin { get; private set; }

    public UpdatePostCommand AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public UpdatePostCommand WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
