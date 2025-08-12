using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Auth;

internal class CommentAuthorizationHandler : ProfileAuthorizationHandlerBase<Comment, CommentId>
{
    public CommentAuthorizationHandler(FeedDbContext context, IPermissionManager permissionManager)
        : base(context, permissionManager)
    {
    }
}
