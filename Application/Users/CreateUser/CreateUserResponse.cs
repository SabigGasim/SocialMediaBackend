using SocialMediaBackend.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Application.Users.CreateUser;

public record CreateUserResponse(
    Guid Id,
    string Username,
    string Nickname,
    DateOnly DateOfBirth,
    Media ProfilePicture
    );
