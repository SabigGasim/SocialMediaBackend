using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.Modules.Payments.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.EventBus;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Payments;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Processing;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Quartz;

namespace SocialMediaBackend.Modules.Payments.Application.Configuration;

public static class PaymentsStartup
{
    public static async Task InitializeAsync(
        IServiceCollection services,
        string connectionString,
        IWebHostEnvironment environment)
    {
        ConfigureCompositionRoot(services, connectionString, environment);

        await QuartzStartup.InitializeAsync();
        EventBusStartup.Initialize();
    }

    private static void ConfigureCompositionRoot(IServiceCollection services, string connectionString, IHostEnvironment env)
    {
        var builder = new ContainerBuilder();

        builder.Populate(services);

        builder.RegisterModule(new PersistenceModule(connectionString, env));
        builder.RegisterModule(new ProcessingModule());
        builder.RegisterModule(new QuartzModule());
        builder.RegisterModule(new CQRSModule());
        builder.RegisterModule(new PaymentsAutofacModule());
        builder.RegisterModule(new EventBusModule());

        var container = builder.Build();

        PaymentsCompositionRoot.SetContainer(container);
    }
}