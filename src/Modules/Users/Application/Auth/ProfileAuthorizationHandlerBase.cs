using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Domain.Common;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Auth;
public abstract class ProfileAuthorizationHandlerBase<TUserResource, TId>
    : IAuthorizationHandler<TUserResource, TId>
    where TUserResource : Entity<TId>, IUserResource
    where TId : class
{
    protected readonly ApplicationDbContext _context;

    public ProfileAuthorizationHandlerBase(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual Task<bool> AuthorizeAsync(UserId? userId, TId resourceId, AuthOptions options, CancellationToken ct = default)
    {
        var queryable = _context
            .Set<TUserResource>()
            .AsNoTracking();

        var fullQuery = AuthorizeQueryable(queryable, userId, resourceId, options);

        return fullQuery.AnyAsync(ct);
    }

    public virtual Task<bool> IsAdminOrResourceOwnerAsync(UserId? userId, TId resourceId, AuthOptions options, CancellationToken ct = default)
    {
        if (UserIsAdmin(userId, options))
        {
            return Task.FromResult(true);
        }

        return _context
            .Set<TUserResource>()
            .AsNoTracking()
            .Where(x => x.Id == resourceId && x.User.Id == userId)
            .AnyAsync(ct);
    }

    public virtual IQueryable<TUserResource> AuthorizeQueryable(IQueryable<TUserResource> queryable, UserId? userId, AuthOptions options)
    {
        if (UserIsAdmin(userId, options))
        {
            return queryable;
        }

        if (UserIsNotAdmin(userId, options))
        {
            return queryable.Where(x =>
                       x.UserId == userId
                    || x.User.ProfileIsPublic
                    || x.User.Followers.Any(x => x.FollowerId == userId));
        }

        return queryable.Where(x => x.User.ProfileIsPublic);
    }

    public virtual IQueryable<TUserResource> AuthorizeQueryable(IQueryable<TUserResource> queryable, UserId? userId, TId resourceId, AuthOptions options)
    {
        queryable = queryable
            .Where(x => x.Id == resourceId);

        return AuthorizeQueryable(queryable, userId, options);
    }

    private static bool UserIsAdmin(UserId? userId, AuthOptions options)
    {
        return userId is not null && options.IsAdmin;
    }

    private static bool UserIsNotAdmin(UserId? userId, AuthOptions options)
    {
        return userId is not null && !options.IsAdmin;
    }
}
