using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequet;

public class RejectFollowRequestCommandHandler(
    UsersDbContext context,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RejectFollowRequestCommand>
{
    private readonly UsersDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<HandlerResponse> ExecuteAsync(RejectFollowRequestCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(x => x.Followers.Where(f => f.Status == FollowStatus.Pending))
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        var rejected = user.RejectPendingFollowRequest(command.UserToRejectId);
        if (!rejected)
            return ("User with the given Id did not send a follow request", HandlerResponseStatus.Conflict, command.UserToRejectId);

        await _unitOfWork.CommitAsync(ct);

        return HandlerResponseStatus.Deleted;
    }
}
