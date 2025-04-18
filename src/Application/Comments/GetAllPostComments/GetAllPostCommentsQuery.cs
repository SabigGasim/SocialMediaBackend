using SocialMediaBackend.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsQuery(Guid postId, int page, int pageSize) : QueryBase<GetAllPostCommentsResponse>
{
    public Guid PostId { get; } = postId;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
