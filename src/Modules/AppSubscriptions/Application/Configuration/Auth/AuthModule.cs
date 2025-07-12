using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.Users;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Security;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Configuration.Auth;

public class AuthModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserContext>()
            .As<IUserContext>()
            .InstancePerLifetimeScope();

        builder.RegisterType<PermissionManager>()
            .As<IPermissionManager>()
            .SingleInstance();
    }
}
