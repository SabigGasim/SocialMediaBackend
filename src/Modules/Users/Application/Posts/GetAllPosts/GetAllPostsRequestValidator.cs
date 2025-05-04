using FastEndpoints;
using FluentValidation;
using SocialMediaBackend.Modules.Users.Application.Common;

namespace SocialMediaBackend.Modules.Users.Application.Posts.GetAllPosts;

public class GetAllPostsRequestValidator : Validator<GetAllPostsRequest>
{
    public GetAllPostsRequestValidator()
    {
        var today = TimeProvider.System.GetUtcNow().ToDateOnly();

        RuleFor(x => x.Until)
            .LessThanOrEqualTo(today)
            .GreaterThanOrEqualTo(new DateOnly(2025, 1, 1));

        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.IdOrUsername) || !string.IsNullOrEmpty(x.Text))
            .WithMessage("You should provide at least one of: Username, UserId, Text");
    }
}
