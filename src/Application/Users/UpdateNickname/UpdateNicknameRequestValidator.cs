﻿using FastEndpoints;
using FluentValidation;

namespace SocialMediaBackend.Application.Users.UpdateNickname;

public class UpdateNicknameRequestValidator : Validator<UpdateNicknameRequest>
{
    public UpdateNicknameRequestValidator()
    {
        RuleFor(x => x.Nickname)
            .NotEmpty();

        RuleFor(x => x.Nickname)
            .Matches(UserRegularExpressions.NicknameRegex());
    }
}
