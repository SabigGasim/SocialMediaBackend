namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;

// They don't implement and IQueryHandler in FastEndpoints
public interface IQueryHandler<in TRequest, TResponse>
    : FastEndpoints.ICommandHandler<TRequest, HandlerResponse<TResponse>>
    where TRequest : IQuery<HandlerResponse<TResponse>>
{

}
