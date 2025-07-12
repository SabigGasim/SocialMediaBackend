using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.GetUser;

public class CheckUserExistsQuery(Guid userId) : QueryBase<UserExistsResponse>
{
    public UserId UserId { get; } = new(userId);
}
