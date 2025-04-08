using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Application.Users.GetUser;

public class GetUserRequestValidator : Validator<GetUserRequest>
{
    public GetUserRequestValidator()
    {
        RuleFor(x => x.IdOrUsername)
            .NotEmpty();

        RuleFor(x => x.IdOrUsername.Length)
            .GreaterThan(3);
    }
}
