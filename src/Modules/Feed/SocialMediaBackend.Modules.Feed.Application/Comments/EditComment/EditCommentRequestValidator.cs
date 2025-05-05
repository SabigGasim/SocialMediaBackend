using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.EditComment;

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
