using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Modules.Users.Application.Comments.CreateComment;

public class CreateCommentRequestValidator : Validator<CreateCommentRequest>
{
    public CreateCommentRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty();

        RuleFor(x => x.Text.Length)
            .InclusiveBetween(1, 150);
    }
}
