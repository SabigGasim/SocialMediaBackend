using FluentValidation;
using SocialMediaBackend.Application.Common;

namespace SocialMediaBackend.Application.Users.GetAllUsers;

public class GetAllUsersRequestValidator : PagedRequestValidator<GetAllUsersRequest>
{
    public GetAllUsersRequestValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty();
    }
}
