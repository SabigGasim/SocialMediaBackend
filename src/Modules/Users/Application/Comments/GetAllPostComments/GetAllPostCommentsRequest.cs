using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Modules.Users.Application.Abstractions;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllPostComments;

public record GetAllPostCommentsRequest : PagedRequest
{
    [FromRoute]
    public required Guid PostId { get; init; }
}
