using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Posts.GetPost;

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
