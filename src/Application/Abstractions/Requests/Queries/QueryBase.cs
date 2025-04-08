namespace SocialMediaBackend.Application.Abstractions.Requests.Queries;

public class QueryBase<TResponse> : IQuery<HandlerResponse<TResponse>>
{
    public Guid Id { get; }

    protected QueryBase(Guid id) => Id = id;

    protected QueryBase() => Id = Guid.NewGuid();
}
