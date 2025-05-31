using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Follows;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Comments;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Posts;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common;

public static class DbContextExtensions
{
    public static async Task<(Author user, Author follower, Follow follow)> CreateAuthorWithFollowerAsync(
        this DbContext context, FollowStatus status = FollowStatus.Following)
    {

        var user = AuthorFactory.Create(isPublic: status == FollowStatus.Following);
        var follower = AuthorFactory.Create();

        var follow = Follow.Create(
            follower.Id,
            user.Id,
            DateTimeOffset.UtcNow,
            status);

        context.Add(user);
        context.Add(follower);
        context.Add(follow);

        await context.SaveChangesAsync();

        return (user, follower, follow);
    }

    public static async Task<(Author user, List<Author> followers, List<Follow> follows)> CreateAuthorWithFollowerAsync(
        this DbContext context, FollowStatus status = FollowStatus.Following, int followersCount = 1)
    {

        var user = AuthorFactory.Create(isPublic: status == FollowStatus.Following);
        var followers = new List<Author>(followersCount);
        var follows = new List<Follow>(followersCount);

        for (int i = 0; i < followersCount; i++)
        {
            var follower = AuthorFactory.Create();
                var follow = Follow.Create(
                follower.Id,
                user.Id,
                DateTimeOffset.UtcNow,
                status);

            followers.Add(follower);
            follows.Add(follow);
        }

        context.Add(user);
        context.AddRange(followers);
        context.AddRange(follows);
        await context.SaveChangesAsync();

        return (user, followers, follows);
    }

    public static async Task<Comment> CreateCommentAsync(
        this DbContext context, PostId? postId = null, AuthorId? userId = null, CancellationToken ct = default)
    {
        if (userId is null)
        {
            var user = AuthorFactory.Create();
            userId = user.Id;
            context.Add(user);
        }

        if (postId is null)
        {
            var post = PostFactory.Create(userId);
            postId = post.Id;
            context.Add(post);
        }

        var comment = CommentFactory.Create(postId, userId);

        context.Add(comment);

        await context.SaveChangesAsync(ct);

        return comment;
    }

    public static async Task<(Comment comment, Comment reply)> CreateCommentWithReplyAsync(
        this DbContext context, CancellationToken ct = default)
    {
        var user = AuthorFactory.Create();
        var replier = AuthorFactory.Create();
        var post = PostFactory.Create(user.Id);
        var comment = CommentFactory.Create(post.Id, user.Id);
        var reply = comment.AddReply(replier.Id, "text")!;

        context.Add(user);
        context.Add(replier);
        context.Add(post);
        context.Add(comment);
        context.Add(reply);

        await context.SaveChangesAsync(ct);

        return (comment, reply);
    }
}
