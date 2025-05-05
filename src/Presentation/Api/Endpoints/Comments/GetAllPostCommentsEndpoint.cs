using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;
using SocialMediaBackend.Modules.Users.Api.Endpoints;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Posts.GetAllPostComments)]
public class GetAllPostCommentsEndpoint : RequestEndpoint<GetAllPostCommentsRequest, GetAllPostCommentsResponse>
{
    public override Task HandleAsync(GetAllPostCommentsRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetAllPostCommentsQuery(req.PostId, req.Page, req.PageSize), ct);
    }
}
