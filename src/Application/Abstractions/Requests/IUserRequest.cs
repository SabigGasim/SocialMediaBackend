namespace SocialMediaBackend.Application.Abstractions.Requests;

public interface IUserRequest<TResult> : IRequest<TResult>
{
    Guid UserId { get; }
    void SetUserId(Guid userId);
}