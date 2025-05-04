using SocialMediaBackend.Modules.Users.Application.Users.GetUser;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Application.Posts.GetPost;

public record GetPostResponse(
    Guid PostId,
    string? Text,
    IEnumerable<string>? MediaUrls,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    int LikesCount,
    int CommentsCount,
    GetUserResponse User
    );
