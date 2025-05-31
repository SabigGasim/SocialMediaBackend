using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Authors;

public interface IAuthorRepository
{
    Task<AuthorDto?> GetByIdAsync(AuthorId authorId, CancellationToken ct = default);
}
