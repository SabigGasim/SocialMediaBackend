using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

public abstract class ProfileAuthorizationHandlerBase<TUserResource, TId>
    : IAuthorizationHandler<TUserResource, TId>
    where TUserResource : Entity<TId>, IUserResource
    where TId : class
{
    protected readonly DbContext _context;
    private readonly IPermissionManager _permissionManager;

    public ProfileAuthorizationHandlerBase(DbContext context, IPermissionManager permissionManager)
    {
        _context = context;
        _permissionManager = permissionManager;
    }

    public virtual async Task<AuthResult> AuthorizeAsync(AuthorId? authorId, TId resourceId, CancellationToken ct = default)
    {
        var queryable = _context
            .Set<TUserResource>()
            .AsNoTracking();

        var fullQuery = await AuthorizeQueryable(queryable, authorId, resourceId);

        return await fullQuery.AnyAsync(ct)
            ? AuthResult.Success()
            : AuthResult.Fail();
    }

    public virtual async Task<AuthResult> IsAdminOrResourceOwnerAsync(
        AuthorId? authorId,
        TId resourceId, 
        CancellationToken ct = default)
    {
        if (await UserIsAdminAsync(authorId, ct))
        {
            return AuthResult.Admin();
        }

        var isResourceOwner = await _context
            .Set<TUserResource>()
            .AsNoTracking()
            .Where(x => x.Id == resourceId && x.AuthorId == authorId)
            .AnyAsync(ct);

        return isResourceOwner
            ? AuthResult.ResourceOwner()
            : AuthResult.Fail();
    }

    public virtual async Task<IQueryable<TUserResource>> AuthorizeQueryable(
        IQueryable<TUserResource> queryable, 
        AuthorId? authorId, 
        CancellationToken ct = default)
    {
        if (await UserIsAdminAsync(authorId, ct))
        {
            return queryable;
        }

        if (authorId is not null)
        {
            return queryable.Where(x =>
                       x.AuthorId == authorId
                    || x.Author.ProfileIsPublic
                    || x.Author.Followers.Any(x => x.FollowerId == authorId)
                    );
        }

        return queryable.Where(x => x.Author.ProfileIsPublic);
    }

    public virtual Task<IQueryable<TUserResource>> AuthorizeQueryable(
        IQueryable<TUserResource> queryable, 
        AuthorId? authorId, 
        TId resourceId,
        CancellationToken ct = default)
    {
        queryable = queryable
            .Where(x => x.Id == resourceId);

        return AuthorizeQueryable(queryable, authorId, ct);
    }

    private async Task<bool> UserIsAdminAsync(AuthorId? authorId, CancellationToken ct = default)
    {
        if (authorId is not null)
        {
            return await _permissionManager.UserIsInRole(authorId.Value, (int)Roles.AdminAuthor, ct);
        }

        return false;
    }
}
