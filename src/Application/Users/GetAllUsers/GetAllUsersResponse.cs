using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Users.GetUser;

namespace SocialMediaBackend.Application.Users.GetAllUsers;

public record GetAllUsersResponse : PagedResponse<GetUserResponse>
{
    public GetAllUsersResponse(
        int PageNumber,
        int PageSize, 
        int TotalCount, 
        IEnumerable<GetUserResponse> Items) : base(PageNumber, PageSize, TotalCount, Items)
    {
    }
}
