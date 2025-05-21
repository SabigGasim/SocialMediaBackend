namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;

// They don't implement an IQueryHandler in FastEndpoints
public interface IQueryHandler<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IQuery<HandlerResponse<TResponse>>
{

}
