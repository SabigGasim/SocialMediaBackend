using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;

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
