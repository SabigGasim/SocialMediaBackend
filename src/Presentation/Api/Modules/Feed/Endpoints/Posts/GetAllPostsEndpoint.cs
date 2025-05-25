using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

[FastEndpoints.HttpGet(ApiEndpoints.Posts.GetAll)]
public class GetAllPostsEndpoint(IFeedModule module) : RequestEndpoint<GetAllPostsRequest, GetAllPostsResponse>(module)
{
    public override Task HandleAsync(GetAllPostsRequest req, CancellationToken ct)
    {
        var order = req.Order?.Trim().ToLower() switch
        {
            "asc" => Order.Ascending,
            "desc" => Order.Descending,
            _ => Order.Unordered
        };

        var query = new GetAllPostsQuery(
            req.Page, 
            req.PageSize,
            order,
            req.Text, 
            req.Since, 
            req.Until,
            req.IdOrUsername);

        return HandleQueryAsync(query, ct);
    }
}
