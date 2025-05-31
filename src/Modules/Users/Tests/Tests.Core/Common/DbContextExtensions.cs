using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;

public static class DbContextExtensions
{
    public static async Task<(User user, User follower, Follow follow)> CreateUserWithFollowerAsync(
        this UsersDbContext context, FollowStatus status = FollowStatus.Following)
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
        this UsersDbContext context, FollowStatus status = FollowStatus.Following, int followersCount = 1)
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
}