using SocialMediaBackend.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Application.Posts.GetPost;

public record GetPostResponse(
    Guid PostId,
    Guid UserId,
    string Username,
    string Nickname,
    string ProfilePictureUrl,
    IEnumerable<string> MediaUrls,
    DateTimeOffset CreatedAt);
