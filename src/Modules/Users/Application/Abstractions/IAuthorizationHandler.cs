using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Users.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Abstractions;

public interface IAuthorizationHandler<TResource, TResourceId>
    where TResource : Entity<TResourceId>
{
    IQueryable<TResource> AuthorizeQueryable(
        IQueryable<TResource> resource,
        UserId? userId,
        TResourceId resourceId,
        AuthOptions options);
    
    IQueryable<TResource> AuthorizeQueryable(
        IQueryable<TResource> resource,
        UserId? userId,
        AuthOptions options);

    Task<bool> AuthorizeAsync(
        UserId? userId, 
        TResourceId resourceId, 
        AuthOptions options,
        CancellationToken ct = default);

    Task<bool> IsAdminOrResourceOwnerAsync(
        UserId? userId, 
        TResourceId resourceId, 
        AuthOptions options,
        CancellationToken ct = default);
}
