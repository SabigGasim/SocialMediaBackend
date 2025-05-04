using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Domain.Common;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;

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
