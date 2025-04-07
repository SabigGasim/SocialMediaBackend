namespace SocialMediaBackend.Application.Common.Abstractions.Requests.Queries;

//                                           They don't implement and IQueryHandler in FastEndpoints
public interface IQueryHandler<in TRequest, TResponse> : FastEndpoints.ICommandHandler<TRequest, TResponse>
    where TRequest : IQuery<TResponse>
{

}
