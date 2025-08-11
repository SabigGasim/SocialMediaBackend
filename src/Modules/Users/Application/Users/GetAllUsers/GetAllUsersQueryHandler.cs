using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

internal sealed class GetAllUsersQueryHandler(UsersDbContext context) 
    : IQueryHandler<GetAllUsersQuery, GetAllUsersResponse>
{
    private readonly UsersDbContext _context = context;

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
