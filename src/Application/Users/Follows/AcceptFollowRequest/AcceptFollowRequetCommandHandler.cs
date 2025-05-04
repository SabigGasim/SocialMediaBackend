using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows.AcceptFollowRequest;

public class AcceptFollowRequetCommandHandler(
    ApplicationDbContext context,
    IUnitOfWork unitOfWork) : ICommandHandler<AcceptFollowRequetCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<HandlerResponse> ExecuteAsync(AcceptFollowRequetCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(x => x.Followers)
            .ThenInclude(x => x.Follower)
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        var accepted = user.AcceptPendingFollowRequest(command.UserToAcceptId);
        if (!accepted)
        {
            return ("User with the given Id didn't request a follow", HandlerResponseStatus.Conflict, command.UserToAcceptId);
        }

        await _unitOfWork.CommitAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
