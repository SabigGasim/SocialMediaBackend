using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Common;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

public class FollowUserCommandHandler(
    ApplicationDbContext context, 
    IUnitOfWork unitOfWork) : ICommandHandler<FollowUserCommand, FollowUserResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<HandlerResponse<FollowUserResponse>> ExecuteAsync(FollowUserCommand command, CancellationToken ct)
    {
        var userToFollow = await _context.Users
            .Include(x => x.Followers)
            .FirstOrDefaultAsync(x => x.Id == command.UserToFollowId);

        if (userToFollow is null)
        {
            return ("User with the given Id was not found", HandlerResponseStatus.NotFound, command.UserToFollowId);
        }

        var follow = userToFollow.FollowOrRequestFollow(new(command.UserId));
        if (follow is null)
        {
            return ("User is already followed", HandlerResponseStatus.Conflict, command.UserToFollowId);
        }
        
        _context.Add(follow);
        
        await _unitOfWork.CommitAsync(ct);

        return (follow.MapToFollowResponse(), HandlerResponseStatus.Created);
    }
}
