using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Users.Application.Posts.GetPost;

namespace SocialMediaBackend.Modules.Users.Application.Posts.GetAllPosts;

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
