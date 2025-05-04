using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsQuery(Guid postId, int page, int pageSize)
    : QueryBase<GetAllPostCommentsResponse>, IOptionalUserRequest<GetAllPostCommentsQuery>
{
    public PostId PostId { get; } = new(postId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;

    public Guid? UserId { get; private set; }

    public bool IsAdmin { get; private set; }

    public GetAllPostCommentsQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetAllPostCommentsQuery WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
