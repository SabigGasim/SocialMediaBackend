using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

public interface IAuthorizationHandler<TResource, TResourceId>
    where TResource : Entity<TResourceId>, IUserResource
{
    Task<IQueryable<TResource>> AuthorizeQueryable(
        IQueryable<TResource> resource,
        AuthorId? authorId,
        TResourceId resourceId,
        CancellationToken ct = default);

    Task<IQueryable<TResource>> AuthorizeQueryable(
        IQueryable<TResource> resource,
        AuthorId? authorId,
        CancellationToken ct = default);

    Task<AuthResult> AuthorizeAsync(
        AuthorId? authorId,
        TResourceId resourceId,
        CancellationToken ct = default);

    Task<AuthResult> IsAdminOrResourceOwnerAsync(
        AuthorId? authorId,
        TResourceId resourceId,
        CancellationToken ct = default);
}
