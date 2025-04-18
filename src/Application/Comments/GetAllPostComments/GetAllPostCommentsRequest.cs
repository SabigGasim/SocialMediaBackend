using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Abstractions;

namespace SocialMediaBackend.Application.Comments.GetAllPostComments;

public record GetAllPostCommentsRequest : PagedRequest
{
    [FromRoute]
    public required Guid PostId { get; init; }
}
