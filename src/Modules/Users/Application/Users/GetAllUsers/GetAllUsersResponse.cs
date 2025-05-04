using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Users.Application.Users.GetUser;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

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
