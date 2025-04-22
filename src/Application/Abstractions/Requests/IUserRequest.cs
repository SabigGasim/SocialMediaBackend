namespace SocialMediaBackend.Application.Abstractions.Requests;

public interface IUserRequest<TRequest> : IRequest
{
    Guid UserId { get; }
    bool IsAdmin { get; }
    TRequest WithUserId(Guid userId);
    TRequest AndAdminRole(bool isAdmin);
}