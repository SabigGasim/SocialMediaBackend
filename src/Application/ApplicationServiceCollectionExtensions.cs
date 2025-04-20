using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.DomainServices.Users;
using SocialMediaBackend.Application.Processing;
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
            ;
    }
}
