using FluentValidation;
using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

public class GetAllUsersRequestValidator : PagedRequestValidator<GetAllUsersRequest>
{
    public GetAllUsersRequestValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty();
    }
}
