namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

public interface ICommandHandler<in TCommand> : FastEndpoints.ICommandHandler<TCommand, HandlerResponse>
    where TCommand : ICommand<HandlerResponse>
{

}

public interface ICommandHandler<TCommand, TResult> : FastEndpoints.ICommandHandler<TCommand, HandlerResponse<TResult>>
    where TCommand : ICommand<HandlerResponse<TResult>>
{

}