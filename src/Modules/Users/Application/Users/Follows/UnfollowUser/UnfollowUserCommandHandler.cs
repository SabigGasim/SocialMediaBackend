using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

internal sealed class UnfollowUserCommandHandler(UsersDbContext context) : ICommandHandler<UnfollowUserCommand>
{
    private readonly UsersDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(UnfollowUserCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(u => u.Followings)
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        var result = user.Unfollow(command.UserToUnfollow);

        if (!result.IsSuccess)
        {
            return result;
        }

        return HandlerResponseStatus.Deleted;
    }
}
