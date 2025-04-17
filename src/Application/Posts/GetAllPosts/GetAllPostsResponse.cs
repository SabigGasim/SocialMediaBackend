using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Posts.GetPost;

namespace SocialMediaBackend.Application.Posts.GetAllPosts;

public record GetAllPostsResponse : PagedResponse<GetPostResponse>
{
    public GetAllPostsResponse(
        int PageNumber, 
        int PageSize, 
        int TotalCount,
        IEnumerable<GetPostResponse> items) : base(PageNumber, PageSize, TotalCount, items)
    {

    }
}
