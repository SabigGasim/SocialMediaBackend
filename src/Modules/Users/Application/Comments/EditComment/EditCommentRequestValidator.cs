using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Modules.Users.Application.Comments.EditComment;

public class EditCommentRequestValidator : Validator<EditCommentRequest>
{
    public EditCommentRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty();

        RuleFor(x => x.Text.Length)
            .InclusiveBetween(1, 150);
    }
}
