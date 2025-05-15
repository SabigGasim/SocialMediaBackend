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


    public static async Task<User> CreateAsync(string username, string nickname, DateOnly dateOfBirth, 
        IUserExistsChecker userExistsChecker,
        Media? profilePicture = null,
        CancellationToken ct = default)
    {
        await CheckRuleAsync(new UsernameShouldBeUniqueRule(userExistsChecker, username), ct);

        var pfp = profilePicture ?? Media.DefaultProfilePicture;

        return new User(username, nickname, dateOfBirth, pfp);
    }

    public Follow? FollowOrRequestFollow(UserId followerId)
    {
        var followExists = _followers.Any(x => x.FollowerId == followerId);
        if (followExists)
        {
            return null;
        }

        var follow = ProfileIsPublic
            ? Follow.Create(followerId, this.Id)
            : Follow.CreateFollowRequest(followerId, this.Id);

        if(follow.Status == FollowStatus.Following)
            this.AddDomainEvent(new UserFollowedEvent(follow.FollowerId, follow.FollowingId, follow.FollowedAt));

        _followers.Add(follow);

        return follow;
    }

    public bool AcceptFollowRequest(UserId followerId)
    {
        var follow = _followers.Find(x => x.FollowerId == followerId);
        if (follow is null || !follow.AcceptFollowRequest())
        {
            return false;
        }

        this.AddDomainEvent(new FollowRequestAcceptedEvent(follow.FollowerId, follow.FollowingId));

        return true;
    }

    public bool Unfollow(UserId userToUnfollowId)
    {
        var follow = _followings.Find(x => x.FollowingId == userToUnfollowId);

        if(follow is null)
            return false;

        _followings.Remove(follow);

        this.AddDomainEvent(new UserUnfollowedEvent(this.Id, userToUnfollowId));
        return true;
    }

    public void IncrementFollowingCount(int amount) => FollowingCount += amount;
    public void IncrementFollowersCount(int amount) => FollowersCount += amount;

    public bool ChangeProfilePrivacy(bool publicProfile)
    {
        if (publicProfile == ProfileIsPublic)
            return true;

        ProfileIsPublic = publicProfile;

        var succeded = ProfileIsPublic
            ? AcceptAllPendingFollowRequests()
            : true;

        if (succeded)
        {
            this.AddDomainEvent(new UserInfoUpdatedDomainEvent(this));
        }

        return succeded;
    }

    public bool RejectPendingFollowRequest(UserId userToRejectId)
    {
        var follow = _followers.Find(x => x.FollowerId == userToRejectId);
        if(follow is null) 
            return false;

        _followers.Remove(follow);
        this.AddDomainEvent(new FollowRequestRejectedEvent(userToRejectId, this.Id));

        return true;
    }

    public bool AcceptPendingFollowRequest(UserId userToAcceptId)
    {
        var followRequest = _followers.Find(x => x.FollowerId == userToAcceptId);
        if (followRequest is null)
            return false;

        var accepted = followRequest.AcceptFollowRequest();
        if(!accepted)
            return false;

        this.AddDomainEvent(new FollowRequestAcceptedEvent(userToAcceptId, this.Id));
        return true;
    }

    public bool AcceptAllPendingFollowRequests()
    {
        foreach (var follow in _followers.Where(x => x.Status == FollowStatus.Pending))
        {
            follow.AcceptFollowRequest();
            this.AddDomainEvent(new FollowRequestAcceptedEvent(follow.FollowerId, follow.FollowingId));
        }

        return true;
    }

    public async Task<bool> ChangeUsernameAsync(string username, IUserExistsChecker userExistsChecker,
        CancellationToken ct = default)
    {
        if(Username == username)
        {
            return false;
        }

        await CheckRuleAsync(new UsernameShouldBeUniqueRule(userExistsChecker, username), ct);

        Username = username;
        this.AddDomainEvent(new UserInfoUpdatedDomainEvent(this));

        return true;
    }

    public bool ChangeNickname(string nickname)
    {
        if (Nickname == nickname)
        {
            return false;
        }

        Nickname = nickname;
        this.AddDomainEvent(new UserInfoUpdatedDomainEvent(this));

        return true;
    }

    public void Delete()
    {
        this.AddDomainEvent(new UserDeletedDomainEvent(this.Id));
    }
}
