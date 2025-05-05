using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

public class PostDto
{
    public Guid Id { get; init; }
    public string? Text { get; init; } = default!;
    public int LikesCount { get; init; }
    public int CommentsCount { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
    public string? MediaUrls { get; set; }
    public AuthorDto Author { get; set; } = default!;
}
