using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
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
            .FromSqlInterpolated($"""
                SELECT * FROM "Users" WHERE "Username" || "Nickname" ILIKE '%' || {query.Slug} || '%'
            """)
            .AsNoTracking();

        var totalCount = await sqlQuery.CountAsync();

        var users = await sqlQuery
            .OrderBy(u => u.Id)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return users.MapToResponse(query.Page, query.PageSize, totalCount);
    }
}
