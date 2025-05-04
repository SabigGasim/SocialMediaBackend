using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Comments.GetAllReplies;

public class GetAllRepliesQuery(Guid parentId, int page, int pageSize) 
    : QueryBase<GetAllRepliesResponse>, IOptionalUserRequest<GetAllRepliesQuery>
{
    public CommentId ParentId { get; } = new(parentId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;

    public UserId? UserId { get; private set; } = default!;

    public bool IsAdmin {  get; private set; }

    public GetAllRepliesQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetAllRepliesQuery WithUserId(Guid userId)
    {
        UserId = new(userId);
        return this;
    }
}
