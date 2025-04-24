using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Domain.Posts;

namespace SocialMediaBackend.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsQuery(Guid postId, int page, int pageSize) : QueryBase<GetAllPostCommentsResponse>
{
    public PostId PostId { get; } = new(postId);
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
}
