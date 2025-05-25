using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequest;

public class RejectFollowRequestCommandHandler(UsersDbContext context)
    : ICommandHandler<RejectFollowRequestCommand>
{
    private readonly UsersDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(RejectFollowRequestCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(x => x.Followers.Where(f => f.Status == FollowStatus.Pending))
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        var result = user.RejectPendingFollowRequest(command.UserToRejectId);
        if (!result.IsSuccess)
        {
            return result;
        }

        return HandlerResponseStatus.Deleted;
    }
}
