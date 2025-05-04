using FluentValidation;
using SocialMediaBackend.Modules.Users.Application.Common;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

public class GetAllUsersRequestValidator : PagedRequestValidator<GetAllUsersRequest>
{
    public GetAllUsersRequestValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty();
    }
}
