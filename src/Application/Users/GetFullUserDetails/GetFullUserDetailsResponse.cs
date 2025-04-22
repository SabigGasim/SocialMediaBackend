namespace SocialMediaBackend.Application.Users.GetFullUserDetails;
public record GetFullUserDetailsResponse(
    Guid UserId,
    string Username,
    string Nickname,
    int FollowersCount,
    int FollowingCount,
    DateOnly DateOfBirth,
    string ProfilePictureUrl,
    bool ProfileIsPublic);
