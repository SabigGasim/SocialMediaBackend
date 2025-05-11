using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.GetReplies)]
public class GetAllRepliesEndpoint(IFeedModule module) : RequestEndpoint<GetAllRepliesRequest, GetAllRepliesResponse>(module)
{
    public override Task HandleAsync(GetAllRepliesRequest req, CancellationToken ct)
    {
        return HandleQueryAsync(new GetAllRepliesQuery(req.ParentId, req.Page, req.PageSize), ct);
    }
}
