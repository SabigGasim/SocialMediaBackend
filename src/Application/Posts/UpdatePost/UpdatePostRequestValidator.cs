﻿using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Application.Posts.UpdatePost;

public class UpdatePostRequestValidator : Validator<UpdatePostRequest>
{
    public UpdatePostRequestValidator()
    {
        RuleFor(x => x.Text.Length)
            .InclusiveBetween(1, 150);
    }
}
