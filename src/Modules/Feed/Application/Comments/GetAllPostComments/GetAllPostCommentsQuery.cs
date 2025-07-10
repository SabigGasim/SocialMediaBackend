using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;

public sealed class GetAllPostCommentsQuery(Guid postId, int page, int pageSize) 
    : QueryBase<GetAllPostCommentsResponse>, IOptionalUserRequest
{
    public PostId PostId { get; } = new(postId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;

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
