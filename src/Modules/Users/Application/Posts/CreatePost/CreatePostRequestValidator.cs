using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Modules.Users.Application.Posts.CreatePost;

public sealed class CreatePostRequestValidator : Validator<CreatePostRequest>
{
    public CreatePostRequestValidator()
    {
        RuleFor(x => x.Text)
            .MaximumLength(150);

        RuleFor(x => x.MediaItems.Count())
            .LessThanOrEqualTo(4);

        RuleFor(x => x)
            .Must(x => PostHasMediaItems(x) || PostHasText(x));
    }


    private static bool PostHasText(CreatePostRequest x)
    {
        return x.Text.Length > 0;
    }

    private static bool PostHasMediaItems(CreatePostRequest x)
    {
        return x.MediaItems.Count() > 0;
    }
}
