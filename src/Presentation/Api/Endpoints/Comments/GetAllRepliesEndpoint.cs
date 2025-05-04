using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.GetAllReplies;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.GetReplies)]
public class GetAllRepliesEndpoint : RequestEndpoint<GetAllRepliesRequest, GetAllRepliesResponse>
{
    public override Task HandleAsync(GetAllRepliesRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetAllRepliesQuery(req.ParentId, req.Page, req.PageSize), ct);
    }
}
