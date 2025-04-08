using SocialMediaBackend.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Application.Users.GetAllUsers;

public class GetAllUsersQuery : PagedQueryBase<GetAllUsersResponse>
{
    public GetAllUsersQuery(string slug, int page, int pageSize) : base(page, pageSize)
    {
        Slug = slug;
    }

    public string Slug { get; }
}
