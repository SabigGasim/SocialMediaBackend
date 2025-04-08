using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Abstractions;

namespace SocialMediaBackend.Application.Users.GetAllUsers;

public record GetAllUsersRequest : PagedRequest
{
    public GetAllUsersRequest(
        [FromQuery]string slug,
        [FromQuery]int pageNumber,
        [FromQuery]int pageSize) : base(pageNumber, pageSize)
    {
        Slug = slug;
    }

    public string Slug { get; set; }
}
