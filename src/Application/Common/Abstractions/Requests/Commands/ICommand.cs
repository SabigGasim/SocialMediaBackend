namespace SocialMediaBackend.Application.Common.Abstractions.Requests.Commands;

public interface ICommand : IRequest
{
    Guid Id { get; }
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
    Guid Id { get; }
}