using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Application.Users.UpdateUsername;

public class UpdateUsernameRequestValidator : Validator<UpdateUsernameRequest>
{
    public UpdateUsernameRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty();

        RuleFor(x => x.Username)
            .Matches(UserRegularExpressions.UsernameRegex());
    }
}
