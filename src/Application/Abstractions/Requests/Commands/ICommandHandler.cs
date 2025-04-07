using SocialMediaBackend.Application.Abstractions.Requests;

namespace SocialMediaBackend.Application.Abstractions.Requests.Commands;

public interface ICommandHandler<in TRequest> : FastEndpoints.ICommandHandler<TRequest>
    where TRequest : ICommand
{

}

public interface ICommandHandler<in TRequest, TResponse> : FastEndpoints.ICommandHandler<TRequest, HandlerResponse<TResponse>>
    where TRequest : ICommand<HandlerResponse<TResponse>>
{

}