using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

public record GetPostResponse(
    Guid PostId,
    string? Text,
    IEnumerable<string>? MediaUrls,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    int LikesCount,
    int CommentsCount,
    GetAuthorResponse Author
    );
