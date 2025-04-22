using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.GetFullUserDetails;

public class GetFullUserDetailsQueryHandler(ApplicationDbContext context)
    : IQueryHandler<GetFullUserDetailsQuery, GetFullUserDetailsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<GetFullUserDetailsResponse>> ExecuteAsync(GetFullUserDetailsQuery query, CancellationToken ct)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstAsync(x => x.Id == query.UserId, ct);

        return (user.MapToFullUserResponse(), HandlerResponseStatus.OK);
    }
}
