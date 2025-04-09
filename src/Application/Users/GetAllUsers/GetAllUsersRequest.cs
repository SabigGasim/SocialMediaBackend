using FastEndpoints;
using SocialMediaBackend.Application.Abstractions;

namespace SocialMediaBackend.Application.Users.GetAllUsers;

public record GetAllUsersRequest : PagedRequest
{
    [QueryParam, BindFrom("slug")]
    public required string Slug { get; init; }
}
