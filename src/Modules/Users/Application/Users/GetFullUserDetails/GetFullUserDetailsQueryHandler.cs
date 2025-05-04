using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;

public class GetFullUserDetailsQueryHandler(ApplicationDbContext context)
    : IQueryHandler<GetFullUserDetailsQuery, GetFullUserDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<GetFullUserDetailsResponse>> ExecuteAsync(GetFullUserDetailsQuery query, CancellationToken ct)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstAsync(x => x.Id == new UserId(query.UserId), ct);

        return (user.MapToFullUserResponse(), HandlerResponseStatus.OK);
    }
}
