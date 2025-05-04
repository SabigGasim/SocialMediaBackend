using SocialMediaBackend.Modules.Users.Infrastructure.Common;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Domain.Posts;

public interface IPostRepository
{
    Task<PagedDto<PostDto>> GetAllAsync(GetAllPostsOptions options, CancellationToken ct = default);
}
