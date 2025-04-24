using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Auth;

internal class PostAuthorizationHandler(ApplicationDbContext context)
    : ProfileAuthorizationHandlerBase<Post, PostId>(context);