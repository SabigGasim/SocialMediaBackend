using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

internal class AuthorizationService(IServiceScopeFactory serviceScopeFactory) : IAuthorizationService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    public async Task<bool> AuthorizeAsync<TResource, TResourceId>(AuthorId? userId, TResourceId resourceId, AuthOptions options, CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
        return await handler.AuthorizeAsync(userId, resourceId, options, ct);
    }

    public async Task<bool> IsAdminOrResourceOwnerAsync<TResource, TResourceId>(AuthorId? userId, TResourceId resourceId, AuthOptions options, CancellationToken ct = default)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
        return await handler.AuthorizeAsync(userId, resourceId, options, ct);
    }

    public IQueryable<TResource> AuthorizeQueryable<TResource, TResourceId>(IQueryable<TResource> resource, AuthorId? userId, TResourceId resourceId, AuthOptions options)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
        return handler.AuthorizeQueryable(resource, userId, resourceId, options);
    }

    public IQueryable<TResource> AuthorizeQueryable<TResource, TResourceId>(IQueryable<TResource> resource, AuthorId? userId, AuthOptions options)
        where TResource : Entity<TResourceId>, IUserResource
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<IAuthorizationHandler<TResource, TResourceId>>();
        return handler.AuthorizeQueryable(resource, userId, options);
    }
}
