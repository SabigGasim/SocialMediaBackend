using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

public class GetPostQuery(Guid postId) : QueryBase<GetPostResponse>, IOptionalUserRequest
{
    public PostId PostId { get; } = new(postId);
    public Guid? UserId { get; private set; }
    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
