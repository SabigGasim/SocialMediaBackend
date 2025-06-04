using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Payments.Infrastructure;
using System.Reflection;

namespace SocialMediaBackend.Modules.Payments.Application.Configuration.Mediator;

public class CQRSModule : Autofac.Module
{
    //As the time writing this comment, Autofac fails to register
    //the UnitOfWorkCommandHandlerWithResultDecorator, as t seems
    //to have poor support for complex open generic registerations.
    //Normally, we would have the handler registerations under a
    //MediatorModule, and the decorators under the ProcessingModule,
    //Now that we're doing both in the same module, I decided to call
    //it the CQRSModule.

    protected override void Load(ContainerBuilder builder)
    {
        List<Assembly> assemblies =
        [
            typeof(IApplicationMarker).Assembly,
            typeof(IInfrastructureMarker).Assembly
        ];

        var services = new ServiceCollection();

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        //services.Decorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>));

        builder.Populate(services);
    }

}
