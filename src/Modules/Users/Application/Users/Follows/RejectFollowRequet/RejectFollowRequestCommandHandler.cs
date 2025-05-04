using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Domain.Common;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequet;

public class RejectFollowRequestCommandHandler(
    ApplicationDbContext context,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RejectFollowRequestCommand>
{
    private readonly ApplicationDbContext _context = context;
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
