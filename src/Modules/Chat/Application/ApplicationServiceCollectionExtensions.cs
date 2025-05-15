using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Chat.Application.Contracts;

namespace SocialMediaBackend.Modules.Chat.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddSingleton<IChatModule, ChatModule>();
    }
}
