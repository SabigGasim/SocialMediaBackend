using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Infrastructure;
using System.Reflection;

namespace SocialMediaBackend.Modules.Chat.Application.Configuration.Mediator;

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

        //services.Decorate(typeof(IQueryHandler<,>), typeof(AuthQueryHandlerDecorator<,>));
        //services.Decorate(typeof(ICommandHandler<>), typeof(AuthCommandHandlerDecorator<>));
        //services.Decorate(typeof(ICommandHandler<,>), typeof(AuthCommandHandlerWithResultDecorator<,>));

        builder.Populate(services);
    }
}
