using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetUser;

public class GetUserHandler : IQueryHandler<GetUserQuery, GetUserResponse>
{
    private readonly UsersDbContext _context;

    public GetUserHandler(UsersDbContext context)
    {
        _context = context;
    }

    public async Task<HandlerResponse<GetUserResponse>> ExecuteAsync(GetUserQuery query, CancellationToken ct)
    {
        var usersQueryable = _context.Users.AsNoTracking();
        var parsed = Guid.TryParse(query.IdOrUsername, out var userId);

        var user = parsed
            ? await usersQueryable.FirstOrDefaultAsync(x => x.Id == new UserId(userId), ct)
            : await usersQueryable.FirstOrDefaultAsync(x => x.Username == query.IdOrUsername, ct);

        return user is not null
            ? user.MapToGetResponse()
            : ("User was not found", HandlerResponseStatus.NotFound, query.IdOrUsername);
    }
}
