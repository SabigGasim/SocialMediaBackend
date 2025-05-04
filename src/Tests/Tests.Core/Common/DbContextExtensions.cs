using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using Tests.Core.Common.Comments;
using Tests.Core.Common.Posts;
using Tests.Core.Common.Users;

namespace Tests.Core.Common;

public static class DbContextExtensions
{
    public static async Task<(User user, User follower, Follow follow)> CreateUserWithFollowerAsync(
        this FakeDbContext context, FollowStatus status = FollowStatus.Following)
    {

        var user = await UserFactory.CreateAsync(isPublic: status == FollowStatus.Following);
        var follower = await UserFactory.CreateAsync();
        var follow = status == FollowStatus.Following
            ? Follow.Create(follower.Id, user.Id)
            : Follow.CreateFollowRequest(follower.Id, user.Id);

        context.Add(user);
        context.Add(follower);
        context.Add(follow);
        await context.SaveChangesAsync();

        return (user, follower, follow);
    }

    public static async Task<(User user, List<User> followers, List<Follow> follows)> CreateUserWithFollowerAsync(
        this FakeDbContext context, FollowStatus status = FollowStatus.Following, int followersCount = 1)
    {

        var user = await UserFactory.CreateAsync(isPublic: status == FollowStatus.Following);
        var followers = new List<User>(followersCount);
        var follows = new List<Follow>(followersCount);
        
        for (int i = 0; i < followersCount; i++)
        {
            var follower = await UserFactory.CreateAsync();
            var follow = status == FollowStatus.Following
                ? Follow.Create(follower.Id, user.Id)
                : Follow.CreateFollowRequest(follower.Id, user.Id);
            
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
        this FakeDbContext context, PostId? postId = null, UserId? userId = null, CancellationToken ct = default)
    {
        if (userId is null)
        {
            var user = await UserFactory.CreateAsync();
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
        this FakeDbContext context, CancellationToken ct = default)
    {
        var user = await UserFactory.CreateAsync();
        var replier = await UserFactory.CreateAsync();
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
