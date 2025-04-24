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
        IQueryable<Post> resource, 
        UserId? subjectId, 
        PostId? resourceId, 
        AuthOptions options)
    {
        var queryable = resource
            .Where(x => x.Id == resourceId);

        if (IsNonAdminAuthenticatedUser(subjectId, options))
        {
            queryable = queryable
                .Where(x =>
                x.UserId == subjectId
                    || x.User.ProfileIsPublic
                    || x.User.Followers.Any(x => x.FollowerId == subjectId));
        }
        else if (UserIsNotAuthenticated(subjectId))
        {
            queryable = queryable.Where(x => x.User.ProfileIsPublic);
        }

        return queryable;
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



    private static bool IsNonAdminAuthenticatedUser(UserId? userId, AuthOptions options)
    {
        return userId is not null && !options.IsAdmin;
    }

    private static bool UserIsNotAuthenticated(UserId? userId)
    {
        return userId is null;
    }
}
