using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Chat.Domain.Follows;

namespace SocialMediaBackend.Modules.Chat.Domain.Chatters;

public sealed class Chatter : AggregateRoot<ChatterId>
{
    private readonly List<Follow> _followers = new();
    private readonly List<Follow> _followings = new();

    private Chatter() { }

    private Chatter(
        ChatterId chatterId,
        string username,
        string nickname,
        Media profilePicture,
        bool profileIsPublic,
        int followersCount,
        int followingCount) : base()
    {
        Id = chatterId;
        Username = username;
        Nickname = nickname;
        ProfilePicture = profilePicture;
        ProfileIsPublic = profileIsPublic;
        FollowersCount = followersCount;
        FollowingCount = followingCount;
    }

    public static Chatter Create(
        ChatterId chatterId,
        string username,
        string nickname,
        Media profilePicture,
        bool profileIsPublic,
        int followersCount,
        int followingCount)
    {
        return new Chatter(chatterId, username, nickname, profilePicture, profileIsPublic, followersCount, followingCount);
    }

    public IReadOnlyCollection<Follow> Followers => _followers.AsReadOnly();
    public IReadOnlyCollection<Follow> Followings => _followings.AsReadOnly();

    public string Username { get; private set; } = default!;
    public string Nickname { get; private set; } = default!;
    public Media ProfilePicture { get; private set; } = default!;
    public bool ProfileIsPublic { get; private set; }
    public int FollowersCount { get; private set; }
    public int FollowingCount { get; private set; }
}
