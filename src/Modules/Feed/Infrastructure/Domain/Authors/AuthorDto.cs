namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Authors;

public class AuthorDto
{
    public Guid Id { get; set; }
    public string Username { get; init; } = default!;
    public string Nickname { get; init; } = default!;
    public int FollowersCount { get; init; } = 0;
    public int FollowingCount { get; init; } = 0;
    public string ProfilePictureUrl { get; init; } = default!;
}
