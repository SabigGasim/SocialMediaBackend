using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;

public record GetAllRepliesResponse : PagedResponse<GetReplyShortResponse>
{
    public GetAllRepliesResponse(Guid parentId, PagedResponse<GetReplyShortResponse> original) : base(original)
    {
        ParentId = parentId;
    }

    public GetAllRepliesResponse(
        Guid parentId,
        int PageNumber,
        int PageSize,
        int TotalCount,
        IEnumerable<GetReplyShortResponse> items) : base(PageNumber, PageSize, TotalCount, items)
    {
        ParentId = parentId;
    }

    public Guid ParentId { get; }
}
