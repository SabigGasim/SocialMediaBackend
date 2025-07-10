using Autofac;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Security;

namespace SocialMediaBackend.Modules.Feed.Application.Configuration.Auth;

public class AuthModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AuthorContext>()
            .As<IAuthorContext>()
            .InstancePerLifetimeScope();

        builder.RegisterType<CommentAuthorizationHandler>()
            .As<IAuthorizationHandler<Comment, CommentId>>()
            .InstancePerLifetimeScope();

        builder.RegisterType<PostAuthorizationHandler>()
            .As<IAuthorizationHandler<Post, PostId>>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AuthorizationService>()
            .As<IAuthorizationService>()
            .SingleInstance();

        builder.RegisterType<PermissionManager>()
            .As<IPermissionManager>()
            .SingleInstance();
    }
}
