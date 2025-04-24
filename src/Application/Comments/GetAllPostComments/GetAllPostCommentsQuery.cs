using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsQuery(Guid postId, int page, int pageSize)
    : QueryBase<GetAllPostCommentsResponse>, IOptionalUserRequest<GetAllPostCommentsQuery>
{
    public PostId PostId { get; } = new(postId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;

    public UserId? UserId { get; private set; }

    public bool IsAdmin { get; private set; }

    public GetAllPostCommentsQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetAllPostCommentsQuery WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
