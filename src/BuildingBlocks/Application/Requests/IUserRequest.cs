namespace SocialMediaBackend.BuildingBlocks.Application.Requests;
public interface IUserRequestBase : IRequest, IRequireAuthorization
{
    void WithUserId(Guid userId);
    void WithAdminRole(bool isAdmin);
}

public interface IOptionalUserRequest : IUserRequestBase
{
    Guid? UserId { get; }
    bool IsAdmin { get; }
}