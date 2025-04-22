using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows.FollowUser;

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
            
        var userIsAlreadyFollowed = userToFollow.Followers.Any(x => x.FollowerId == command.UserId);
        if (userIsAlreadyFollowed)
        {
            return ("User is already followed", HandlerResponseStatus.Conflict, command.UserToFollowId);
        }

        var follow = userToFollow.FollowOrRequestFollow(command.UserId);
        
        _context.Add(follow);
        
        await _unitOfWork.CommitAsync(ct);

        return (follow.MapToFollowResponse(), HandlerResponseStatus.Created);
    }
}
