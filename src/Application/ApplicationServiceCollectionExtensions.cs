using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Auth;
using SocialMediaBackend.Application.DomainServices.Users;
using SocialMediaBackend.Application.Processing;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Services;

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
            .AddScoped<IAuthorizationHandler<Post, PostId>, PostAuthorizationHandler>()
            .AddScoped<IAuthorizationHandler<Comment, CommentId>, CommentAuthorizationHandler>()
            .AddSingleton<IAuthorizationService, AuthorizationService>()
            .AddMediator(o => o.ServiceLifetime = ServiceLifetime.Scoped)
            ;
    }
}
