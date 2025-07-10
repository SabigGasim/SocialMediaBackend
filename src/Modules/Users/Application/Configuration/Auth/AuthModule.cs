using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Security;

namespace SocialMediaBackend.Modules.Users.Application.Configuration.Auth;

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
