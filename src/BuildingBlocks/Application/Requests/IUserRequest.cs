namespace SocialMediaBackend.BuildingBlocks.Application.Requests;
public interface IUserRequestBase : IRequest
{
    void WithUserId(Guid userId);
    void WithAdminRole(bool isAdmin);
}

public interface IUserRequest : IUserRequestBase
{
    Guid UserId { get; }
    bool IsAdmin { get; }
}


public interface IOptionalUserRequest : IUserRequestBase
{
    Guid? UserId { get; }
    bool IsAdmin { get; }
}