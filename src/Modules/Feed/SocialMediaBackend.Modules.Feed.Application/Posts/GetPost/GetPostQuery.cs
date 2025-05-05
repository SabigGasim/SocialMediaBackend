using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

public class GetPostQuery(Guid postId) : QueryBase<GetPostResponse>, IOptionalUserRequest<GetPostQuery>
{
    public PostId PostId { get; } = new(postId);
    public Guid? UserId { get; private set; }
    public bool IsAdmin { get; private set; }

    public GetPostQuery WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }

    public GetPostQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }
}
