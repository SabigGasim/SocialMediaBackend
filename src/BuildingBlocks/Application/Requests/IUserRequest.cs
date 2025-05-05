namespace SocialMediaBackend.BuildingBlocks.Application.Requests;
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