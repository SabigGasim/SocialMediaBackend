using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;

public class AcceptFollowRequestCommandHandler(UsersDbContext context) : ICommandHandler<AcceptFollowRequestCommand>
{
    private readonly UsersDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(AcceptFollowRequestCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(x => x.Followers)
            .ThenInclude(x => x.Follower)
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        var accepted = user.AcceptPendingFollowRequest(command.UserToAcceptId);
        if (!accepted)
        {
            return ("User with the given Id didn't request a follow", HandlerResponseStatus.NotFound, command.UserToAcceptId);
        }

        return HandlerResponseStatus.NoContent;
    }
}
