using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Users.IntegrationEvents;

public sealed class UserInforUpdatedIntegrationEvent : IntegrationEvent
{
    public UserInforUpdatedIntegrationEvent(
        Guid userId,
        string username,
        string nickname,
        DateOnly dateOfBirth,
        Media profilePicture,
        bool profileIsPublic) : base()
    {
        UserId = userId;
        Username = username;
        Nickname = nickname;
        DateOfBirth = dateOfBirth;
        ProfilePicture = profilePicture;
        ProfileIsPublic = profileIsPublic;
    }

    public Guid UserId { get; }
    public string Username { get; }
    public string Nickname { get; }
    public DateOnly DateOfBirth { get; }
    public Media ProfilePicture { get; }
    public bool ProfileIsPublic { get; }
}