using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Services;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Domain.Users.Rules;

namespace SocialMediaBackend.Domain.Users;

public class User : AggregateRoot<Guid>
{
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();
    private readonly List<Follow> _followers = new();
    private readonly List<Follow> _followings = new();

    private User(string username, string nickname, DateOnly dateOfBirth, Media profilePicture)
    {
        Username = username;
        Nickname = nickname;
        DateOfBirth = dateOfBirth;
        ProfilePicture = profilePicture;
        ProfileIsPublic = true;

        Id = Guid.NewGuid();
        Created = DateTimeOffset.UtcNow;
        CreatedBy = "System";
        LastModified = DateTimeOffset.UtcNow;
        LastModifiedBy = "System";
    }

    private User() { }

    public string Username { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;

    public DateOnly DateOfBirth { get; private set; }
    public Media ProfilePicture { get; private set; } = default!;
    public bool ProfileIsPublic { get; private set; }
    public int FollowersCount { get; private set; }
    public int FollowingCount { get; private set; }

    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<Follow> Followers => _followers.AsReadOnly();
    public IReadOnlyCollection<Follow> Followings => _followings.AsReadOnly();


    public static async Task<User> CreateAsync(string username, string nickname, DateOnly dateOfBirth, 
        IUserExistsChecker userExistsChecker,
        Media? profilePicture = null)
    {
        await CheckRuleAsync(new UsernameShouldBeUniqueRule(userExistsChecker, username));

        var pfp = profilePicture ?? Media.DefaultProfilePicture;

        return new User(username, nickname, dateOfBirth, pfp);
    }

    public Post? AddPost(string? text = null, IEnumerable<Media>? mediaItems = null)
    {
        var post = Post.Create(Id, text, mediaItems as List<Media> ?? mediaItems?.ToList());
        if (post is null)
            return null;

        _posts.Add(post);
        return post;
    }

    public bool RemovePost(Guid postId)
    {
        var post = _posts.Find(x => x.Id == postId)!;
        return _posts.Remove(post);
    }

    public Follow FollowOrRequestFollow(Guid userId)
    {
        var follow = ProfileIsPublic
            ? Follow.Create(userId, this.Id)
            : Follow.CreateFollowRequest(userId, this.Id);

        if(follow.Status == FollowStatus.Following)
            this.AddDomainEvent(new UserFollowedEvent(follow.FollowerId, follow.FollowingId));

        return follow;
    }

    public bool AcceptFollowRequest(Guid followerId)
    {
        var follow = _followers.Find(x => x.FollowerId == followerId);

        return follow?.AcceptFollowRequest() == true;
    }

    public bool Unfollow(Guid followingId)
    {
        var follow = _followers.Find(x => x.FollowingId == followingId);

        return follow is not null
            ? _followers.Remove(follow)
            : false;
    }

    public void IncrementFollowingCount(int amount) => FollowingCount += amount;
    public void IncrementFollowersCount(int amount) => FollowersCount += amount;

    public bool ChangeProfilePrivacy(bool publicProfile)
    {
        if (publicProfile == ProfileIsPublic)
            return true;

        ProfileIsPublic = publicProfile;

        return ProfileIsPublic
            ? AcceptAllPendingFollowRequests()
            : true;
    }

    public bool RejectPendingFollowRequest(Guid followerId)
    {
        var follow = _followers.Find(x => x.FollowerId == followerId);
        
        return follow is not null
            ? _followers.Remove(follow)
            : false;
    }

    public bool AcceptPendingFollowRequest(Guid followerId)
    {
        return _followers.Find(x => x.FollowerId == followerId)?.AcceptFollowRequest() == true;
    }

    public bool AcceptAllPendingFollowRequests()
    {
        foreach (var follow in _followers.Where(x => x.Status == FollowStatus.Pending))
        {
            follow.AcceptFollowRequest();
        }

        return true;
    }

    public async Task<bool> ChangeUsernameAsync(string username, IUserExistsChecker userExistsChecker)
    {
        if(Username == username)
        {
            return false;
        }

        await CheckRuleAsync(new UsernameShouldBeUniqueRule(userExistsChecker, username));

        return true;
    }

    public bool ChangeNickname(string nickname)
    {
        if (Nickname == nickname)
        {
            return false;
        }

        Nickname = nickname;
        return true;
    }
}
