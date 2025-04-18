﻿namespace SocialMediaBackend.Api.Contracts.Responses;

internal record ValidationFailureResponse
{
    public List<string> Errors { get; init; } = new();
}
