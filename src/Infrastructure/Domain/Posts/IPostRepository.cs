using SocialMediaBackend.Infrastructure.Common;

namespace SocialMediaBackend.Infrastructure.Domain.Posts;

public interface IPostRepository
{
    Task<PagedDto<PostDto>> GetAllAsync(GetAllPostsOptions options, CancellationToken ct = default);
}
