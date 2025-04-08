using SocialMediaBackend.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Application.Users.GetUser;

public class GetUserQuery(string idOrUsername) : QueryBase<GetUserResponse>
{
    public string IdOrUsername { get; } = idOrUsername;
}
