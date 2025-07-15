using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Users.IntegrationEvents;

public sealed class UserCreatedIntegrationEvent(
    Guid userId,
    string username,
    string nickname,
    DateOnly dateOfBirth,
    Media profilePicture,
    bool profileIsPublic,
    int followersCount,
    int followingCount) : IntegrationEvent()
{
    public Guid UserId { get; } = userId;
    public string Username { get; } = username;
    public string Nickname { get; } = nickname;
    public DateOnly DateOfBirth { get; } = dateOfBirth;
    public Media ProfilePicture { get; } = profilePicture;
    public bool ProfileIsPublic { get; } = profileIsPublic;
    public int FollowersCount { get; } = followersCount;
    public int FollowingCount { get; } = followingCount;
}
