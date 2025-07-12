using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Configuration.Auth;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.AppSubscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.EventBus;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Processing;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Quartz;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Configuration;

public static class AppSubscriptionsStartup
{
    public static async Task InitializeAsync(
        IServiceCollection services,
        string connectionString,
        IWebHostEnvironment environment,
        IExecutionContextAccessor contextAccessor)
    {
        ConfigureCompositionRoot(services, connectionString, contextAccessor);

        await PersistenceStartup.InitializeAsync(environment);
        await QuartzStartup.InitializeAsync();
        EventBusStartup.Initialize();
    }

    private static void ConfigureCompositionRoot(IServiceCollection services, string connectionString, IExecutionContextAccessor contextAccessor)
    {
        var builder = new ContainerBuilder();

        builder.Populate(services);

        builder.RegisterModule(new PersistenceModule(connectionString));
        builder.RegisterModule(new ProcessingModule());
        builder.RegisterModule(new QuartzModule());
        builder.RegisterModule(new AppSubscriptionsAutofacModule());
        builder.RegisterModule(new EventBusModule());
        builder.RegisterModule(new AuthModule());
        builder.RegisterModule(new CQRSModule());

        builder.RegisterInstance(contextAccessor);

        var container = builder.Build();

        AppSubscriptionsCompositionRoot.SetContainer(container);
    }
}
