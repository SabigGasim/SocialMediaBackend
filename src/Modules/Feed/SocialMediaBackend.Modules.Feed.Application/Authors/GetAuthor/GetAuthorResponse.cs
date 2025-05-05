namespace SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

public record GetAuthorResponse(
    Guid Id,
    string Username,
    string Nickname,
    int FollowersCount,
    int FollowingCount,
    string? ProfilePictureUrl = null);
