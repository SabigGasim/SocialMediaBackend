using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common.Abstractions.Requests;
using SocialMediaBackend.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Application.Users.CreateUser;

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
