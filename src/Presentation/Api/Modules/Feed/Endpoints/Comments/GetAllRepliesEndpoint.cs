using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.GetReplies)]
public class GetAllRepliesEndpoint : RequestEndpoint<GetAllRepliesRequest, GetAllRepliesResponse>
{
    public override Task HandleAsync(GetAllRepliesRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetAllRepliesQuery(req.ParentId, req.Page, req.PageSize), ct);
    }
}
