using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Auth;
using SocialMediaBackend.Application.Auth.Posts;
using SocialMediaBackend.Application.DomainServices.Users;
using SocialMediaBackend.Application.Processing;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Services;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddCommandMiddleware(x =>
            {
                x.Register(typeof(UserRequestMiddleware<,>));
            })
            .AddSingleton<IUserExistsChecker, UserExistsChecker>()
            .AddScoped<IAuthorizationService<Post, PostId>, PostAuthorizationService>()
            .AddMediator(o => o.ServiceLifetime = ServiceLifetime.Scoped)
            ;
    }
}
