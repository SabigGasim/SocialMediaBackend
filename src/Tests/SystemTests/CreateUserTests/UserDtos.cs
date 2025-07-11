namespace SocialMediaBackend.Tests.SystemTests.CreateUserTests;

public record UserInfo(Guid Id, string Username, string Nickname, string ProfilePicture);
public record PayerInfo(Guid Id, bool IsDeleted);
