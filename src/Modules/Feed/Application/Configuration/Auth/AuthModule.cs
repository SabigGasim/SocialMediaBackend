using Autofac;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Application.Configuration.Auth;

public class AuthModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CommentAuthorizationHandler>()
            .As<IAuthorizationHandler<Comment, CommentId>>()
            .InstancePerDependency();

        builder.RegisterType<PostAuthorizationHandler>()
            .As<IAuthorizationHandler<Post, PostId>>()
            .InstancePerDependency();

        builder.RegisterType<AuthorizationService>()
            .As<IAuthorizationService>()
            .SingleInstance();
    }
}
