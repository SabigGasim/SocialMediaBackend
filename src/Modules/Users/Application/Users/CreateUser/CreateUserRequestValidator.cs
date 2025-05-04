using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public partial class CreateUserRequestValidator : Validator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .Matches(UserRegularExpressions.UsernameRegex())
            .WithMessage("Username should contain from 3 to 12 letters, numbers and underscores!");

        RuleFor(x => x.Nickname)
            .NotEmpty()
            .Matches(UserRegularExpressions.NicknameRegex());

        RuleFor(x => new DateTimeOffset(x.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
            .LessThan(DateTimeOffset.UtcNow.AddYears(-18))
            .WithMessage("User should be older than 18!");
    }
}
