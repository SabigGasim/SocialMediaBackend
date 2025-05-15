using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;
public abstract class ProfileAuthorizationHandlerBase<TUserResource, TId>
    : IAuthorizationHandler<TUserResource, TId>
    where TUserResource : Entity<TId>, IUserResource
    where TId : class
{
    protected readonly FeedDbContext _context;

    public ProfileAuthorizationHandlerBase(FeedDbContext context)
    {
        _context = context;
    }

    public virtual Task<bool> AuthorizeAsync(AuthorId? authorId, TId resourceId, AuthOptions options, CancellationToken ct = default)
    {
        var queryable = _context
            .Set<TUserResource>()
            .AsNoTracking();

        var fullQuery = AuthorizeQueryable(queryable, authorId, resourceId, options);

        return fullQuery.AnyAsync(ct);
    }

    public virtual Task<bool> IsAdminOrResourceOwnerAsync(AuthorId? authorId, TId resourceId, AuthOptions options, CancellationToken ct = default)
    {
        if (UserIsAdmin(authorId, options))
        {
            return Task.FromResult(true);
        }

        return _context
            .Set<TUserResource>()
            .AsNoTracking()
            .Where(x => x.Id == resourceId && x.AuthorId == authorId)
            .AnyAsync(ct);
    }

    public virtual IQueryable<TUserResource> AuthorizeQueryable(IQueryable<TUserResource> queryable, AuthorId? authorId, AuthOptions options)
    {
        if (UserIsAdmin(authorId, options))
        {
            return queryable;
        }

        if (UserIsNotAdmin(authorId, options))
        {
            return queryable.Where(x =>
                       x.AuthorId == authorId
                    || x.Author.ProfileIsPublic
                    || x.Author.Followers.Any(x => x.FollowerId == authorId)
                    );
        }

        return queryable.Where(x => x.Author.ProfileIsPublic);
    }

    public virtual IQueryable<TUserResource> AuthorizeQueryable(IQueryable<TUserResource> queryable, AuthorId? authorId, TId resourceId, AuthOptions options)
    {
        queryable = queryable
            .Where(x => x.Id == resourceId);

        return AuthorizeQueryable(queryable, authorId, options);
    }

    private static bool UserIsAdmin(AuthorId? authorId, AuthOptions options)
    {
        return authorId is not null && options.IsAdmin;
    }

    private static bool UserIsNotAdmin(AuthorId? authorId, AuthOptions options)
    {
        return authorId is not null && !options.IsAdmin;
    }
}
