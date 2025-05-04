using SocialMediaBackend.Modules.Users.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public record CreateUserRequest(
    string Username,
    string Nickname, 
    DateOnly DateOfBirth, 
    Media? ProfilePicture);
