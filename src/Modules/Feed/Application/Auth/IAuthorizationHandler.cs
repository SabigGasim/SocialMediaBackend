using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

public interface IAuthorizationHandler<TResource, TResourceId>
    where TResource : Entity<TResourceId>, IUserResource
{
    IQueryable<TResource> AuthorizeQueryable(
        IQueryable<TResource> resource,
        AuthorId? authorId,
        TResourceId resourceId,
        AuthOptions options);

    IQueryable<TResource> AuthorizeQueryable(
        IQueryable<TResource> resource,
        AuthorId? authorId,
        AuthOptions options);

    Task<bool> AuthorizeAsync(
        AuthorId? authorId,
        TResourceId resourceId,
        AuthOptions options,
        CancellationToken ct = default);

    Task<bool> IsAdminOrResourceOwnerAsync(
        AuthorId? authorId,
        TResourceId resourceId,
        AuthOptions options,
        CancellationToken ct = default);
}
