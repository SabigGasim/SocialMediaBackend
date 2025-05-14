namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand<HandlerResponse>
{

}

public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<HandlerResponse<TResult>>
{

}