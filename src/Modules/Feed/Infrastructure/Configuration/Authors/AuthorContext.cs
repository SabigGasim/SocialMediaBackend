using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Authors;

public sealed class AuthorContext(IExecutionContextAccessor context) : IAuthorContext
{
    private readonly IExecutionContextAccessor _context = context;

    public AuthorId AuthorId => new AuthorId(_context.UserId);
}
