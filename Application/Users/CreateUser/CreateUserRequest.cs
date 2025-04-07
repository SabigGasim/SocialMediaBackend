using SocialMediaBackend.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Application.Users.CreateUser;

public record CreateUserRequest(
    string Username,
    string Nickname, 
    DateOnly DateOfBirth, 
    Media? ProfilePicture);
