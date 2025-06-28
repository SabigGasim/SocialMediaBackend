using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Application.Auth;
using SocialMediaBackend.Modules.Users.Infrastructure;

namespace SocialMediaBackend.Modules.Users.Application.Configuration.Mediator;

public class CQRSModule : Module
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
        var services = new ServiceCollection();

        var assemblies = new[]
        {
            typeof(IApplicationMarker).Assembly,
            typeof(IInfrastructureMarker).Assembly
        };

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

        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventNotification<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.Decorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerWithResultDecorator<,>));

        services.Decorate(typeof(IQueryHandler<,>), typeof(AuthQueryHandlerDecorator<,>));
        services.Decorate(typeof(ICommandHandler<>), typeof(AuthCommandHandlerDecorator<>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(AuthCommandHandlerWithResultDecorator<,>));

        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        builder.Populate(services);
    }

}
