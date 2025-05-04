using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Posts.GetPost;

public class GetPostQuery(Guid postId) : QueryBase<GetPostResponse>, IOptionalUserRequest<GetPostQuery>
{
    public PostId PostId { get; } = new(postId);
    public UserId? UserId { get; private set; }
    public bool IsAdmin { get; private set; }

    public GetPostQuery WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }

    public GetPostQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }
}
