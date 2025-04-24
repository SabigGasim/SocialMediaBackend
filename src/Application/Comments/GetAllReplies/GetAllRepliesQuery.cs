using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Comments;

namespace SocialMediaBackend.Application.Comments.GetAllReplies;

public class GetAllRepliesQuery(Guid parentId, int page, int pageSize) : QueryBase<GetAllRepliesResponse>
{
    public CommentId ParentId { get; } = new(parentId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
