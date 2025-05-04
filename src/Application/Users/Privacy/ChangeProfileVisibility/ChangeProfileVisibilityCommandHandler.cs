using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Privacy.ChangeProfileVisibility;

public class ChangeProfileVisibilityCommandHandler(
    ApplicationDbContext context,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ChangeProfileVisibilityCommand>
{
    private readonly ApplicationDbContext _context = context;
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
