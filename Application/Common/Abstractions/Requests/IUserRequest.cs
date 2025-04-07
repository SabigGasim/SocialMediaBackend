namespace SocialMediaBackend.Application.Common.Abstractions.Requests;

public interface IUserRequest
{
    Guid UserId { get; }
    void WithUserId(Guid userId);
}