using SocialMediaBackend.Application.Abstractions.Requests;

namespace SocialMediaBackend.Application.Abstractions.Requests.Commands;

public interface ICommand : IRequest
{
    Guid Id { get; }
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
    Guid Id { get; }
}