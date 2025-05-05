using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

public interface IPostRepository
{
    Task<PagedDto<PostDto>> GetAllAsync(GetAllPostsOptions options, CancellationToken ct = default);
}
