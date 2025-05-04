using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Domain.Posts;

public class PostDto
{
    public Guid Id { get; init; }
    public string? Text { get; init; } = default!;
    public int LikesCount { get; init; }
    public int CommentsCount { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
    public string? MediaUrls { get; set; }
    public UserDto User { get; set; } = default!;
}
