using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
public interface IUserRequestBase<TRequest> : IRequest
{
    TRequest WithUserId(Guid userId);
    TRequest AndAdminRole(bool isAdmin);
}

public interface IUserRequest<TRequest> : IUserRequestBase<TRequest>
{
    Guid UserId { get; }
    bool IsAdmin { get; }
}


public interface IOptionalUserRequest<TRequest> : IUserRequestBase<TRequest>
{
    Guid? UserId { get; }
    bool IsAdmin { get; }
}