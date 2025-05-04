namespace SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;

public interface IQuery<TResponse> : IRequest<TResponse>
{
    Guid Id { get; }
}
