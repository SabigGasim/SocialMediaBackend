using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Security;

namespace SocialMediaBackend.Modules.Chat.Application.Configuration.Auth;

public class AuthModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ChatterContext>()
            .As<IChatterContext>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(IApplicationMarker).Assembly)
            .AsClosedTypesOf(typeof(IAuthorizationHandler<>))
            .SingleInstance();
    
        builder.RegisterAssemblyTypes(typeof(IApplicationMarker).Assembly)
            .AsClosedTypesOf(typeof(IAuthorizationHandler<,>))
            .SingleInstance();

        builder.RegisterType<PermissionManager>()
            .As<IPermissionManager>()
            .SingleInstance();
    }
}
