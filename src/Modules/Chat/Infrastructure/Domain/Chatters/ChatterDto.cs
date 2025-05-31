namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

public class ChatterDto
{
    public Guid Id { get; init; }
    public string Username { get; init; } = default!;
    public string Nickname { get; init; } = default!;
    public int FollowersCount { get; init; } = 0;
    public int FollowingCount { get; init; } = 0;
    public string ProfilePictureUrl { get; init; } = default!;
}