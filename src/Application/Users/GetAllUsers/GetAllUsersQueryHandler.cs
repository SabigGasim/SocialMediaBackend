using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.GetAllUsers;

public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, GetAllUsersResponse>
{

    private readonly ApplicationDbContext _context;

    public GetAllUsersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HandlerResponse<GetAllUsersResponse>> ExecuteAsync(GetAllUsersQuery query, CancellationToken ct)
    {
        var sqlQuery = _context.Users
            .AsNoTracking()
            .Where(x => EF.Functions.ILike(x.Username, $"%{query.Slug}%")
                || EF.Functions.ILike(x.Nickname, $"%{query.Slug}%"));

        var totalCount = await sqlQuery.CountAsync(ct);

        var users = await sqlQuery
            .OrderBy(u => u.Id)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return users.MapToResponse(query.Page, query.PageSize, totalCount);
    }
}
