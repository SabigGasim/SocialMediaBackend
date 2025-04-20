using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.GetUser;

public class GetUserHandler : IQueryHandler<GetUserQuery, GetUserResponse>
{
    private readonly ApplicationDbContext _context;

    public GetUserHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HandlerResponse<GetUserResponse>> ExecuteAsync(GetUserQuery query, CancellationToken ct)
    {
        var usersQueryable = _context.Users.AsNoTracking();
        var parsed = Guid.TryParse(query.IdOrUsername, out var userId);

        var user = parsed
            ? await usersQueryable.FirstOrDefaultAsync(x => x.Id == userId, ct)
            : await usersQueryable.FirstOrDefaultAsync(x => x.Username == query.IdOrUsername, ct);

        return user is not null
            ? user.MapToGetResponse()
            : ("User was not found", HandlerResponseStatus.NotFound, query.IdOrUsername);
    }
}
