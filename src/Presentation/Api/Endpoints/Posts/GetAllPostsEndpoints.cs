using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Posts.GetAllPosts;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Posts;

[FastEndpoints.HttpGet(ApiEndpoints.Posts.GetAll)]
public class GetAllPostsEndpoints : RequestEndpoint<GetAllPostsRequest, GetAllPostsResponse>
{
    public override Task HandleAsync(GetAllPostsRequest req, CancellationToken ct)
    {
        var order = req.Order?.Trim().ToLower() switch
        {
            "asc" => Order.Ascending,
            "desc" => Order.Descending,
            _ => Order.Unordered
        };

        var query = new GetAllPostsQuery(req.Page, req.PageSize, order,
            req.Text, req.Since, req.Until, req.IdOrUsername);

        return HandleRequestAsync(query, ct);
    }
}
