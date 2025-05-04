using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public record CreateUserRequest(
    string Username,
    string Nickname, 
    DateOnly DateOfBirth, 
    Media? ProfilePicture);
