using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Domain.Common;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

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
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        var followed = user.Unfollow(command.UserToUnfollow);

        if (!followed)
            return ("User with the given Id isn't followed", HandlerResponseStatus.Conflict, command.UserToUnfollow);

        await _unitOfWork.CommitAsync(ct);

        return HandlerResponseStatus.Deleted;
    }
}
