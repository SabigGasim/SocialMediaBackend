using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;

internal sealed class GetFullUserDetailsQueryHandler(
    UsersDbContext context,
    IUserContext userContext)
    : IQueryHandler<GetFullUserDetailsQuery, GetFullUserDetailsResponse>
{
    private readonly UsersDbContext _context = context;
    private readonly IUserContext userContext = userContext;

    public async Task<HandlerResponse<GetFullUserDetailsResponse>> ExecuteAsync(GetFullUserDetailsQuery query, CancellationToken ct)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstAsync(x => x.Id == userContext.UserId, ct);

        return (user.MapToFullUserResponse(), HandlerResponseStatus.OK);
    }
}
