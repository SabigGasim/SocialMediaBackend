namespace SocialMediaBackend.Application.Abstractions.Requests.Queries;

//                                They don't implement an IQuery in FastEndpoints
public interface IQuery<out TResponse> : FastEndpoints.ICommand<TResponse>
{
    Guid Id { get; }
}
