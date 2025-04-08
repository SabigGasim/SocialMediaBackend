namespace SocialMediaBackend.Application.Abstractions.Requests.Commands;

public interface ICommandHandler<in TRequest> : FastEndpoints.ICommandHandler<TRequest, HandlerResponse>
    where TRequest : ICommand<HandlerResponse>
{

}

public interface ICommandHandler<in TRequest, TResponse> : FastEndpoints.ICommandHandler<TRequest, HandlerResponse<TResponse>>
    where TRequest : ICommand<HandlerResponse<TResponse>>
{

}