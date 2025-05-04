using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public class CreateUserCommand(
    string username,
    string nickname,
    DateOnly dateOfBirth, 
    Media? profilePicture) : CommandBase<CreateUserResponse>
{
    public string Username { get; } = username;
    public string Nickname { get; } = nickname;
    public DateOnly DateOfBirth { get; } = dateOfBirth;
    public Media? ProfilePicture { get; } = profilePicture;
}
