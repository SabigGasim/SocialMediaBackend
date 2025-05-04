using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Queries;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetUser;

public class GetUserQuery(string idOrUsername) : QueryBase<GetUserResponse>
{
    public string IdOrUsername { get; } = idOrUsername;
}
