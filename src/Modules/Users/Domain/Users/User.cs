using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users.Events;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Domain.Users.Rules;

namespace SocialMediaBackend.Modules.Users.Domain.Users;

public class User : AggregateRoot<UserId>
{
    private readonly List<Follow> _followers = new();
    private readonly List<Follow> _followings = new();

    private User(string username, string nickname, DateOnly dateOfBirth, Media profilePicture)
    {
        Username = username;
        Nickname = nickname;
        DateOfBirth = dateOfBirth;
        ProfilePicture = profilePicture;
        ProfileIsPublic = true;

        Id = UserId.New();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = "System";

        this.AddDomainEvent(new UserCreatedDomainEvent(
            this.Id.Value,
            Username,
            Nickname,
            ProfilePicture,
            DateOfBirth,
            ProfileIsPublic));
    }

    private User() { }

    public string Username { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;

    public DateOnly DateOfBirth { get; private set; }
    public Media ProfilePicture { get; private set; } = default!;
    public bool ProfileIsPublic { get; private set; }
    public int FollowersCount { get; private set; }
    public int FollowingCount { get; private set; }

    public IReadOnlyCollection<Follow> Followers => _followers.AsReadOnly();
    public IReadOnlyCollection<Follow> Followings => _followings.AsReadOnly();


    public static async Task<Result<User>> CreateAsync(string username, string nickname, DateOnly dateOfBirth, 
        IUserExistsChecker userExistsChecker,
        Media? profilePicture = null,
        CancellationToken ct = default)
    {
        await CheckRuleAsync(new UsernameShouldBeUniqueRule(userExistsChecker, username), ct);

        var pfp = profilePicture ?? Media.DefaultProfilePicture;

        return new User(username, nickname, dateOfBirth, pfp);
    }

    public Result<Follow> FollowOrRequestFollow(UserId followerId)
    {
        var followExists = _followers.Any(x => x.FollowerId == followerId);
        if (followExists)
        {
            return Result<Follow>.Failure(FailureCode.Duplicate, "Follow or follow request");
        }

        var follow = ProfileIsPublic
            ? Follow.Create(followerId, this.Id)
            : Follow.CreateFollowRequest(followerId, this.Id);

        if(follow.Status == FollowStatus.Following)
        {
            this.AddDomainEvent(new UserFollowedEvent(follow.FollowerId, follow.FollowingId, follow.FollowedAt));
        }

        _followers.Add(follow);

        return follow;
    }

    public Result Unfollow(UserId userToUnfollowId)
    {
        var follow = _followings.Find(x => x.FollowingId == userToUnfollowId);

        if(follow is null)
        {
            return Result.Failure(FailureCode.NotFound, "Follow");
        }

        _followings.Remove(follow);

        this.AddDomainEvent(new UserUnfollowedEvent(this.Id, userToUnfollowId));
        
        return Result.Success();
    }

    public void IncrementFollowingCount(int amount) => FollowingCount += amount;
    public void IncrementFollowersCount(int amount) => FollowersCount += amount;

    public Result ChangeProfilePrivacy(bool publicProfile)
    {
        if (publicProfile == ProfileIsPublic)
        {
            return Result.FailureWithMessage(FailureCode.Duplicate, "Profile is already public");
        }

        ProfileIsPublic = publicProfile;

        AcceptAllFollowRequests();

        this.AddDomainEvent(new UserInfoUpdatedDomainEvent(this));

        return Result.Success();
    }

    public Result RejectPendingFollowRequest(UserId userToRejectId)
    {
        var follow = _followers.Find(x => x.FollowerId == userToRejectId);
        if(follow is null)
        {
            return Result.Failure(FailureCode.NotFound, "Follow request");
        }

        _followers.Remove(follow);
        
        this.AddDomainEvent(new FollowRequestRejectedEvent(userToRejectId, this.Id));

        return Result.Success();
    }

    public Result AcceptFollowRequest(UserId userToAcceptId)
    {
        var follow = _followers.Find(x => x.FollowerId == userToAcceptId);
        if (follow is null)
        {
            return Result.Failure(FailureCode.NotFound, "Follow request");
        }

        var result = follow.AcceptFollowRequest();
        if (result.IsSuccess)
        {
            this.AddDomainEvent(new FollowRequestAcceptedEvent(follow.FollowerId, follow.FollowingId));
        }

        return result;
    }

    private void AcceptAllFollowRequests()
    {
        foreach (var follow in _followers.Where(x => x.Status == FollowStatus.Pending))
        {
            follow.AcceptFollowRequest();
            this.AddDomainEvent(new FollowRequestAcceptedEvent(follow.FollowerId, follow.FollowingId));
        }
    }

    public async Task<Result> ChangeUsernameAsync(string username, IUserExistsChecker userExistsChecker,
        CancellationToken ct = default)
    {
        if(Username == username)
        {
            return Result.FailureWithMessage(FailureCode.Duplicate, $"Username is already {username}");
        }

        await CheckRuleAsync(new UsernameShouldBeUniqueRule(userExistsChecker, username), ct);

        Username = username;
        this.AddDomainEvent(new UserInfoUpdatedDomainEvent(this));

        return Result.Success();
    }

    public Result ChangeNickname(string nickname)
    {
        if (Nickname == nickname)
        {
            return Result.FailureWithMessage(FailureCode.NotFound, $"Nickname is already {nickname}");
        }

        Nickname = nickname;
        this.AddDomainEvent(new UserInfoUpdatedDomainEvent(this));

        return Result.Success();
    }

    public void Delete()
    {
        this.AddDomainEvent(new UserDeletedDomainEvent(this.Id));
    }
}
