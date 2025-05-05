using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

namespace SocialMediaBackend.Modules.Feed.Application.Authors.GetAllAuthors;

public record GetAllAuthorsResponse : PagedResponse<GetAuthorResponse>
{
    public GetAllAuthorsResponse(
        int PageNumber,
        int PageSize,
        int TotalCount,
        IEnumerable<GetAuthorResponse> Items) : base(PageNumber, PageSize, TotalCount, Items)
    {
        
    }
}
