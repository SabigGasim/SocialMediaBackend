using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

public interface IAuthorizationService
{
    IQueryable<TResource> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource,
        AuthorId? userId,
        TResourceId resourceId,
        AuthOptions options)
        where TResource : Entity<TResourceId>, IUserResource;

    IQueryable<TResource> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource,
        AuthorId? userId,
        AuthOptions options)
        where TResource : Entity<TResourceId>, IUserResource;

    Task<bool> AuthorizeAsync<TResource, TResourceId>(
        AuthorId? userId,
        TResourceId resourceId,
        AuthOptions options,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource;

    Task<bool> IsAdminOrResourceOwnerAsync<TResource, TResourceId>(
        AuthorId? userId,
        TResourceId resourceId,
        AuthOptions options,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource;
}
