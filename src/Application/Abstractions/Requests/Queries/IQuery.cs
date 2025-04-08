namespace SocialMediaBackend.Application.Abstractions.Requests.Queries;

public interface IQuery<TResponse> : IRequest<TResponse>
{
    Guid Id { get; }
}
