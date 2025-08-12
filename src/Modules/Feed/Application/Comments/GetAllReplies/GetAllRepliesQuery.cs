using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Feed.Domain.Comments;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;

public sealed class GetAllRepliesQuery(Guid parentId, int page, int pageSize)
    : QueryBase<GetAllRepliesResponse>, IRequireOptionalAuthorizaiton
{
    public CommentId ParentId { get; } = new(parentId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
