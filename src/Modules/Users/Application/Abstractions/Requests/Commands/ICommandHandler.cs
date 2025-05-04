namespace SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;

public interface ICommandHandler<in TCommand> : FastEndpoints.ICommandHandler<TCommand, HandlerResponse>
    where TCommand : ICommand<HandlerResponse>
{

}

public interface ICommandHandler<in TCommand, TResponse> : FastEndpoints.ICommandHandler<TCommand, HandlerResponse<TResponse>>
    where TCommand : ICommand<HandlerResponse<TResponse>>
{

}