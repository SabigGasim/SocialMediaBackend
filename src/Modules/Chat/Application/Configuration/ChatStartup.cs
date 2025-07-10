using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Quartz;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Processing;
using SocialMediaBackend.Modules.Chat.Application.Configuration.Auth;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.EventBus;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.SignalR;
using SocialMediaBackend.BuildingBlocks.Application.Auth;

namespace SocialMediaBackend.Modules.Chat.Application.Configuration;

public static class ChatStartup
{
    public static async Task InitializeAsync(
        IServiceCollection serviceCollection,
        string databaseConnection,
        string redisConnection,
        IWebHostEnvironment env,
        IExecutionContextAccessor executionContextAccessor)
    {
        ConfigureCompositionRoot(
            serviceCollection, 
            databaseConnection, 
            redisConnection, 
            executionContextAccessor);

        await PersistenceStartup.InitializeAsync(env);
        await QuartzStartup.InitializeAsync();
        EventBusStartup.Initialize();
    }

    private static void ConfigureCompositionRoot(
        IServiceCollection serviceCollection,
        string databaseConnection,
        string redisConnection,
        IExecutionContextAccessor executionContextAccessor)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.Populate(serviceCollection);

        containerBuilder.RegisterModule(new PersistenceModule(databaseConnection));
        containerBuilder.RegisterModule(new CQRSModule());
        containerBuilder.RegisterModule(new QuartzModule());
        containerBuilder.RegisterModule(new ProcessingModule(redisConnection));
        containerBuilder.RegisterModule(new AuthModule());
        containerBuilder.RegisterModule(new EventBusModule());
        containerBuilder.RegisterModule(new SignalRModule());
        containerBuilder.RegisterModule(new AuthModule());

        containerBuilder.RegisterInstance(executionContextAccessor);

        var container = containerBuilder.Build();

        ChatCompositionRoot.SetContainer(container);
    }
}
