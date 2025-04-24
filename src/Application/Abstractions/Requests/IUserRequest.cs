using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Application.Abstractions.Requests;
public interface IUserRequestBase<TRequest> : IRequest
{
    TRequest WithUserId(Guid userId);
    TRequest AndAdminRole(bool isAdmin);
}

public interface IUserRequest<TRequest> : IUserRequestBase<TRequest>
{
    UserId UserId { get; }
    bool IsAdmin { get; }
}


public interface IOptionalUserRequest<TRequest> : IUserRequestBase<TRequest>
{
    UserId? UserId { get; }
    bool IsAdmin { get; }
}