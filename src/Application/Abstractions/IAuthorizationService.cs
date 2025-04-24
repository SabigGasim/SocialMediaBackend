using SocialMediaBackend.Application.Auth;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Abstractions;

public interface IAuthorizationService<TResource, TResourceId>
    where TResource : Entity<TResourceId>
{
    IQueryable<TResource> AuthorizeQueryable(
        IQueryable<TResource> resource,
        UserId? userId,
        TResourceId resourceId,
        AuthOptions options);

    Task<bool> AuthorizeAsync(
        UserId? userId, 
        TResourceId resourceId, 
        AuthOptions options,
        CancellationToken ct = default);
}
