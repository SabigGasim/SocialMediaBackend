using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Auth;

public interface IAuthorizationService
{
    IQueryable<TResource> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource,
        UserId? userId,
        TResourceId resourceId,
        AuthOptions options)
        where TResource : Entity<TResourceId>;

    IQueryable<TResource> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource,
        UserId? userId,
        AuthOptions options)
        where TResource : Entity<TResourceId>;

    Task<bool> AuthorizeAsync<TResource, TResourceId>(
        UserId? userId,
        TResourceId resourceId,
        AuthOptions options,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>;

    Task<bool> IsAdminOrResourceOwnerAsync<TResource, TResourceId>(
        UserId? userId, 
        TResourceId resourceId, 
        AuthOptions options, 
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>;
}
