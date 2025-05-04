using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Auth;

internal class CommentAuthorizationHandler(ApplicationDbContext context)
    : ProfileAuthorizationHandlerBase<Comment, CommentId>(context);
