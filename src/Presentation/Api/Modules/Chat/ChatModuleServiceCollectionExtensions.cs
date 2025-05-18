using SocialMediaBackend.Modules.Chat.Application;
using SocialMediaBackend.Modules.Chat.Infrastructure;

namespace SocialMediaBackend.Api.Modules.Chat;

internal static class ChatModuleServiceCollectionExtensions
{
    public static IServiceCollection AddChatModule(this IServiceCollection services)
    {
        return services
            .AddApplication()
            .AddInfrastructure();
    }
}
