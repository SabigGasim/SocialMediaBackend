using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpGet(ApiEndpoints.Posts.GetAllPostComments)]
public class GetAllPostCommentsEndpoint(IFeedModule module) : RequestEndpoint<GetAllPostCommentsRequest, GetAllPostCommentsResponse>(module)
{
    public override Task HandleAsync(GetAllPostCommentsRequest req, CancellationToken ct)
    {
        return HandleQueryAsync(new GetAllPostCommentsQuery(req.PostId, req.Page, req.PageSize), ct);
    }
}
