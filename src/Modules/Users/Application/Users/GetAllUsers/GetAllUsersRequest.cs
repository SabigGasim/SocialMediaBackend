using FastEndpoints;
using SocialMediaBackend.Modules.Users.Application.Abstractions;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

public record GetAllUsersRequest : PagedRequest
{
    [QueryParam, BindFrom("slug")]
    public required string Slug { get; init; }
}
