using Autofac;
using SocialMediaBackend.Modules.Chat.Application.Auth;

namespace SocialMediaBackend.Modules.Chat.Application.Configuration.Auth;

public class AuthModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IApplicationMarker).Assembly)
            .AsClosedTypesOf(typeof(IAuthorizationHandler<>))
            .SingleInstance();
    
        builder.RegisterAssemblyTypes(typeof(IApplicationMarker).Assembly)
            .AsClosedTypesOf(typeof(IAuthorizationHandler<,>))
            .SingleInstance();
    }
}
