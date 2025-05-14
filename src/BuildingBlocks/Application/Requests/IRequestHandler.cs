using FastEndpoints;

namespace SocialMediaBackend.BuildingBlocks.Application.Requests;

public interface IRequestHandler<in TRequest> : ICommandHandler<TRequest, HandlerResponse>
    where TRequest : ICommand<HandlerResponse>
{

}

public interface IRequestHandler<TRequest, TResult> : ICommandHandler<TRequest, HandlerResponse<TResult>>
    where TRequest : ICommand<HandlerResponse<TResult>>
{

}
