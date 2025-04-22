namespace SocialMediaBackend.Infrastructure.Domain.Users;

public class UserDto
{
    public Guid UserId { get; set; }
    public string Username { get; init; } = default!;
    public string Nickname { get; init; } = default!;
    public int FollowersCount { get; init; } = 0;
    public int FollowingCount { get; init; } = 0;
    public string ProfilePictureUrl { get; init; } = default!;
}
