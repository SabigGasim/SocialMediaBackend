using SocialMediaBackend.Application.Users.GetUser;
using SocialMediaBackend.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Application.Posts.GetPost;

public record GetPostResponse(
    Guid PostId,
    string? Text,
    IEnumerable<string>? MediaUrls,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    GetUserResponse User);
