using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Auth.Posts;

internal class PostAuthorizationService(ApplicationDbContext context) : IAuthorizationService<Post, PostId>
{
    private readonly ApplicationDbContext _context = context;

    public IQueryable<Post> AuthorizeQueryable(
        IQueryable<Post> queryable, 
        UserId? userId, 
        PostId? resourceId, 
        AuthOptions options)
    {
        queryable = queryable
            .Where(x => x.Id == resourceId);

        if (options.IsAdmin)
        {
            return queryable;
        }

        if (AuthenticatedUserIsNotAdmin(userId, options))
        {
            return queryable.Where(x =>
                       x.UserId == userId
                    || x.User.ProfileIsPublic
                    || x.User.Followers.Any(x => x.FollowerId == userId));
        }
        
        return queryable.Where(x => x.User.ProfileIsPublic);
    }

    public Task<bool> AuthorizeAsync(
        UserId? userId, 
        PostId resourceId, 
        AuthOptions options, 
        CancellationToken ct = default)
    {
        var queryable = _context.Posts.AsNoTracking();

        var fullQuery = AuthorizeQueryable(queryable, userId, resourceId, options);

        return fullQuery.AnyAsync(ct);
    }



    private static bool AuthenticatedUserIsNotAdmin(UserId? userId, AuthOptions options)
    {
        return userId is not null && !options.IsAdmin;
    }
}
