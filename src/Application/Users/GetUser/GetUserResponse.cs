namespace SocialMediaBackend.Application.Users.GetUser;

public record GetUserResponse(
    Guid Id, 
    string Username, 
    string Nickname,
    int FollowersCount,
    int FollowingCount,
    string? ProfilePictureUrl = null);
