using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllPostComments;

public record GetAllPostCommentsRequest : PagedRequest
{
    [FromRoute]
    public required Guid PostId { get; init; }
}
