using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Domain.Users.Events;

public class UserInfoUpdatedDomainEvent : DomainEventBase
{
    public UserInfoUpdatedDomainEvent(User user)
    {
        UserId = user.Id.Value;
        Username = user.Username;
        Nickname = user.Nickname;
        DateOfBirth = user.DateOfBirth;
        ProfilePicture = user.ProfilePicture;
        ProfileIsPublic = user.ProfileIsPublic;
    }

    public UserInfoUpdatedDomainEvent(
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
