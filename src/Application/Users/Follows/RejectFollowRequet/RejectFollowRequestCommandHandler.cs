using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows.RejectFollowRequet;

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
