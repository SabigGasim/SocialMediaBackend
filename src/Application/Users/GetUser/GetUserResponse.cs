using SocialMediaBackend.Domain.Common.ValueObjects;

namespace SocialMediaBackend.Application.Users.GetUser;

public record GetUserResponse(
    Guid Id, 
    string Username, 
    string? Nickname = null,
    Media? ProfilePicture = null);
