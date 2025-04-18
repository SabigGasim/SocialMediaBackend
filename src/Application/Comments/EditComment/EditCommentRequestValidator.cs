using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Application.Comments.EditComment;

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
