using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsRequestValidator : PagedRequestValidator<GetAllPostCommentsRequest>
{
    public GetAllPostCommentsRequestValidator() : base() { }
}
