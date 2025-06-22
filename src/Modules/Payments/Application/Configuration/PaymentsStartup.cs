using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Payments.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;
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
        ConfigureCompositionRoot(services, connectionString);

        await PersistenceStartup.InitializeAsync(environment);
        await QuartzStartup.InitializeAsync();
    }

    private static void ConfigureCompositionRoot(IServiceCollection services, string connectionString)
    {
        var builder = new ContainerBuilder();

        builder.Populate(services);

        builder.RegisterModule(new PersistenceModule(connectionString));
        builder.RegisterModule(new ProcessingModule());
        builder.RegisterModule(new QuartzModule());
        builder.RegisterModule(new CQRSModule());
        builder.RegisterModule(new PaymentsAutofacModule());

        var container = builder.Build();

        PaymentsCompositionRoot.SetContainer(container);
    }
}