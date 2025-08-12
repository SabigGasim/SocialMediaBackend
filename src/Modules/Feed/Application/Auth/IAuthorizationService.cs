using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

public interface IAuthorizationService
{
    Task<IQueryable<TResource>> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource,
        AuthorId? userId,
        TResourceId resourceId,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource;

    Task<IQueryable<TResource>> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource,
        AuthorId? userId,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource;

    Task<AuthResult> AuthorizeAsync<TResource, TResourceId>(
        AuthorId? userId,
        TResourceId resourceId,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource;

    Task<AuthResult> IsAdminOrResourceOwnerAsync<TResource, TResourceId>(
        AuthorId? userId,
        TResourceId resourceId,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource;
}
