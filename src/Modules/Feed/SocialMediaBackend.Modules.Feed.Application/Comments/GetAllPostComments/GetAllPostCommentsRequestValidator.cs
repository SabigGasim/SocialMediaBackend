using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsRequestValidator : PagedRequestValidator<GetAllPostCommentsRequest>
{
    public GetAllPostCommentsRequestValidator() : base() { }
}
