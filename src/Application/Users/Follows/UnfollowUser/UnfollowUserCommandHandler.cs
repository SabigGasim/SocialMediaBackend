using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows.UnfollowUser;

public class UnfollowUserCommandHandler(
    ApplicationDbContext context,
    IUnitOfWork unitOfWork) : ICommandHandler<UnfollowUserCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<HandlerResponse> ExecuteAsync(UnfollowUserCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(u => u.Followings)
            .FirstAsync(x => x.Id == command.UserId, ct);

        var followed = user.Unfollow(command.UserToUnfollow);

        if (!followed)
            return ("User with the given Id isn't followed", HandlerResponseStatus.Conflict, command.UserToUnfollow);

        await _unitOfWork.CommitAsync(ct);

        return HandlerResponseStatus.Deleted;
    }
}
