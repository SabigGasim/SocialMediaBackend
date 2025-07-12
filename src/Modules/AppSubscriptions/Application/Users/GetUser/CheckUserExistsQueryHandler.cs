using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Users.GetUser;

internal sealed class CheckUserExistsQueryHandler(IUserRepository userRepository) : IQueryHandler<CheckUserExistsQuery, UserExistsResponse>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<HandlerResponse<UserExistsResponse>> ExecuteAsync(CheckUserExistsQuery query, CancellationToken ct)
    {
        var exists = await _userRepository.ExistsAsync(query.UserId.Value, ct);

        return new UserExistsResponse(exists);
    }
}
