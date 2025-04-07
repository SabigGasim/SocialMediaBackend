using FastEndpoints;
using FluentValidation;
using System.Text.RegularExpressions;

namespace SocialMediaBackend.Application.Users.CreateUser;
    
public partial class CreateUserRequestValidator : Validator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .Matches(UsernameRegex())
            .WithMessage("Username should contain from 3 to 12 letters, numbers and underscores!");

        RuleFor(x => x.Nickname)
            .NotEmpty()
            .Matches(NicknameRegex())
            .WithMessage("Nickname should contain from 3 to 15 letters, spaces, numbers and underscores!");

        RuleFor(x => new DateTimeOffset(x.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
            .LessThan(DateTimeOffset.UtcNow.AddYears(-18))
            .WithMessage("User should be older than 18!");
    }

    [GeneratedRegex("^[a-zA-Z0-9_ ]{3,15}$", RegexOptions.Compiled)]
    private static partial Regex NicknameRegex();

    [GeneratedRegex("^[a-zA-Z0-9_]{3,12}$", RegexOptions.Compiled)]
    private static partial Regex UsernameRegex();
}
