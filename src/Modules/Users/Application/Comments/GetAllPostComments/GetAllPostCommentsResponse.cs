using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.GetComment;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllPostComments;

public record GetAllPostCommentsResponse : PagedResponse<GetCommentResponse>
{
    public GetAllPostCommentsResponse(PagedResponse<GetCommentResponse> original) : base(original)
    {
    }

    public GetAllPostCommentsResponse(int PageNumber, int PageSize, int TotalCount, IEnumerable<GetCommentResponse> items) : base(PageNumber, PageSize, TotalCount, items)
    {
    }
}
