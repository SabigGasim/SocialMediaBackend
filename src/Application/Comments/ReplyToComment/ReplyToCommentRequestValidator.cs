using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Application.Comments.ReplyToComment;

public class ReplyToCommentRequestValidator : Validator<ReplyToCommentRequest>
{
    public ReplyToCommentRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty();

        RuleFor(x => x.Text.Length)
            .InclusiveBetween(1, 150);
    }
}
