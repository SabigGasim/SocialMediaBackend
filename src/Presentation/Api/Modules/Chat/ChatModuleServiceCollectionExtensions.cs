using SocialMediaBackend.Modules.Chat.Application;

namespace SocialMediaBackend.Api.Modules.Chat;

internal static class ChatModuleServiceCollectionExtensions
{
    public static IServiceCollection AddChatModule(this IServiceCollection services)
    {
        return services.AddApplication();
    }
}
