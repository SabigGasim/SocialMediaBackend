using SocialMediaBackend.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Application.Comments.GetAllReplies;

public class GetAllRepliesQuery(Guid parentId, int page, int pageSize) : QueryBase<GetAllRepliesResponse>
{
    public Guid ParentId { get; } = parentId;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
