using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Users.IntegrationEvents;

public sealed class UserCreatedIntegrationEvent : IntegrationEvent
{
    public UserCreatedIntegrationEvent(
        Guid userId,
        string username,
        string nickname,
        DateOnly dateOfBirth,
        Media profilePicture,
        bool profileIsPublic,
        int followersCount,
        int followingCount) : base()
    {
        UserId = userId;
        Username = username;
        Nickname = nickname;
        DateOfBirth = dateOfBirth;
        ProfilePicture = profilePicture;
        ProfileIsPublic = profileIsPublic;
        FollowersCount = followersCount;
        FollowingCount = followingCount;
    }

    public Guid UserId { get; }
    public string Username { get; }
    public string Nickname { get; }
    public DateOnly DateOfBirth { get; }
    public Media ProfilePicture { get; }
    public bool ProfileIsPublic { get; }
    public int FollowersCount { get; }
    public int FollowingCount { get; }
}
