using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Users.Application.Configuration.Mediator;

public class CQRSModule : Module
{
    //As the time writing this comment, Autofac fails to register
    //the UnitOfWorkCommandHandlerWithResultDecorator decorator, as
    //it seems to poor support for complex open generic registerations.
    //Normally, we would have the handler registerations under a
    //MediatorModule and the decorators under the ProcessingModule,
    //but now that we're doing both in the same module, I decided
    //to call it the CQRSModule.

    protected override void Load(ContainerBuilder builder)
    {
        var services = new ServiceCollection();

        services.Scan(scan => scan
            .FromAssemblyOf<IApplicationMarker>()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblyOf<IApplicationMarker>()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblyOf<IApplicationMarker>()
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>));

        builder.Populate(services);
    }

}
