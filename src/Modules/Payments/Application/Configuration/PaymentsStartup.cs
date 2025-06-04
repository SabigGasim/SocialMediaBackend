using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Quartz;

namespace SocialMediaBackend.Modules.Payments.Application.Configuration;

public static class PaymentsStartup
{
    public static Task InitializeAsync(IServiceCollection services)
    {
        ConfigureCompositionRoot(services);

        return Task.CompletedTask;
    }

    private static void ConfigureCompositionRoot(IServiceCollection services)
    {
        var builder = new ContainerBuilder();

        builder.Populate(services);

        builder.RegisterModule(new QuartzModule());

        var container = builder.Build();

        PaymentsCompositionRoot.SetContainer(container);
    }
}