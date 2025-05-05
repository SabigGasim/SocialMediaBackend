namespace SocialMediaBackend.Api.Contracts;

internal record ValidationFailureResponse
{
    public List<string> Errors { get; init; } = new();
}
