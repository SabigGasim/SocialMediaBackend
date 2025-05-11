using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;
internal class PostAuthorizationHandler : ProfileAuthorizationHandlerBase<Post, PostId>
{
    public PostAuthorizationHandler(FeedDbContext context) : base(context)
    {
    }
}
