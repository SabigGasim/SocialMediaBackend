using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Authors;

public sealed class OptionalAuthorContext(IExecutionContextAccessor contextAccessor) : IOptionalAuthorContext
{
    private readonly IExecutionContextAccessor _contextAccessor = contextAccessor;

    public AuthorId? AuthorId => _contextAccessor.IsAvailable
        ? new(_contextAccessor.UserId)
        : null;
}
