using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpGet(ApiEndpoints.Posts.GetAllPostComments)]
public class GetAllPostCommentsEndpoint : RequestEndpoint<GetAllPostCommentsRequest, GetAllPostCommentsResponse>
{
    public override Task HandleAsync(GetAllPostCommentsRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetAllPostCommentsQuery(req.PostId, req.Page, req.PageSize), ct);
    }
}
