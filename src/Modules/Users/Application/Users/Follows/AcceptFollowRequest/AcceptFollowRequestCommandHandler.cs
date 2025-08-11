using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;

internal sealed class AcceptFollowRequestCommandHandler(
    UsersDbContext context,
    IUserContext userContext) : ICommandHandler<AcceptFollowRequestCommand>
{
    private readonly UsersDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<HandlerResponse> ExecuteAsync(AcceptFollowRequestCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(x => x.Followers)
            .ThenInclude(x => x.Follower)
            .FirstAsync(x => x.Id == _userContext.UserId, ct);

        var result = user.AcceptFollowRequest(command.UserToAcceptId);
        if (!result.IsSuccess)
        {
            return result;
        }

        return HandlerResponseStatus.NoContent;
    }
}
