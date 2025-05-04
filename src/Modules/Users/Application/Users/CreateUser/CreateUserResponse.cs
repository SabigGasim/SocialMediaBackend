using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public record CreateUserResponse(
    Guid Id,
    string Username,
    string Nickname,
    DateOnly DateOfBirth,
    Media ProfilePicture
    );
