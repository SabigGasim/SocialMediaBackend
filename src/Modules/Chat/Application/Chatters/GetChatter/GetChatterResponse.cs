namespace SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

public record GetChatterResponse(
    Guid Id,
    string Username,
    string Nickname,
    int FollowersCount,
    int FollowingCount,
    string? ProfilePictureUrl = null);
