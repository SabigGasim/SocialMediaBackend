using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllReplies;

public class GetAllRepliesQuery(Guid parentId, int page, int pageSize) 
    : QueryBase<GetAllRepliesResponse>, IOptionalUserRequest<GetAllRepliesQuery>
{
    public CommentId ParentId { get; } = new(parentId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;

    public Guid? UserId { get; private set; } = default!;

    public bool IsAdmin {  get; private set; }

    public GetAllRepliesQuery AndAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
        return this;
    }

    public GetAllRepliesQuery WithUserId(Guid userId)
    {
        UserId = userId;
        return this;
    }
}
