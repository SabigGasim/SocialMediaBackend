using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

internal class AuthorizationService(IServiceScopeFactory serviceScopeFactory) : IAuthorizationService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    public async Task<AuthResult> AuthorizeAsync<TResource, TResourceId>(AuthorId? userId, TResourceId resourceId, CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
            return await handler.AuthorizeAsync(userId, resourceId, ct);
        }
    }

    public async Task<AuthResult> IsAdminOrResourceOwnerAsync<TResource, TResourceId>(AuthorId? userId, TResourceId resourceId, CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
            return await handler.IsAdminOrResourceOwnerAsync(userId, resourceId, ct);
        }
    }

    public async Task<IQueryable<TResource>> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource, 
        AuthorId? userId, 
        TResourceId resourceId,
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
            return await handler.AuthorizeQueryable(resource, userId, resourceId, ct);
        }
    }

    public async Task<IQueryable<TResource>> AuthorizeQueryable<TResource, TResourceId>(
        IQueryable<TResource> resource, 
        AuthorId? userId, 
        CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
            return await handler.AuthorizeQueryable(resource, userId, ct);
        }
    }
}
