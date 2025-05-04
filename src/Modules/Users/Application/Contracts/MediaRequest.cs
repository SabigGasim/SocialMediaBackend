namespace SocialMediaBackend.Modules.Users.Application.Contracts;

public record MediaRequest
{
    public required string Url { get; init; }
}
