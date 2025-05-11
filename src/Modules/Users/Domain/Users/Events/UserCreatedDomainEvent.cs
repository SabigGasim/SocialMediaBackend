using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Domain.Users.Events;

public class UserCreatedDomainEvent(
    Guid userId,
    string username,
    string nickname,
    Media profilePicture,
    DateOnly dateOfBirth,
    bool profileIsPublic) : DomainEventBase
{
    public Guid UserId { get; } = userId;
    public string Username { get; } = username;
    public string Nickname { get; } = nickname;
    public Media ProfilePicture { get; } = profilePicture;
    public DateOnly DateOfBirth { get; } = dateOfBirth;
    public bool ProfileIsPublic { get; } = profileIsPublic;
}
