using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.GetAllPostComments;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Posts.GetAllPostComments)]
public class GetAllPostCommentsEndpoint : RequestEndpoint<GetAllPostCommentsRequest, GetAllPostCommentsResponse>
{
    public override Task HandleAsync(GetAllPostCommentsRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetAllPostCommentsQuery(req.PostId, req.Page, req.PageSize), ct);
    }
}
