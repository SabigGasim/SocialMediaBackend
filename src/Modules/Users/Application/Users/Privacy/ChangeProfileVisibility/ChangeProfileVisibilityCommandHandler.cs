using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Privacy.ChangeProfileVisibility;

public class ChangeProfileVisibilityCommandHandler(
    UsersDbContext context,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeProfileVisibilityCommand>
{
    private readonly UsersDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<HandlerResponse> ExecuteAsync(ChangeProfileVisibilityCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(x => x.Followers.Where(x => x.Status == FollowStatus.Pending))
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        user.ChangeProfilePrivacy(command.IsPublic);

        await _unitOfWork.CommitAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
