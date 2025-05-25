using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

public class FollowUserCommandHandler(UsersDbContext context) : ICommandHandler<FollowUserCommand, FollowUserResponse>
{
    private readonly UsersDbContext _context = context;

    public async Task<HandlerResponse<FollowUserResponse>> ExecuteAsync(FollowUserCommand command, CancellationToken ct)
    {
        var userToFollow = await _context.Users
            .Include(x => x.Followers)
            .FirstOrDefaultAsync(x => x.Id == command.UserToFollowId);

        if (userToFollow is null)
        {
            return ("User with the given Id was not found", HandlerResponseStatus.NotFound, command.UserToFollowId);
        }

        var result = userToFollow.FollowOrRequestFollow(new(command.UserId));
        if (!result.IsSuccess)
        {
            return result;
        }

        var follow = result.Payload;

        _context.Add(follow);
        
        return (follow.MapToFollowResponse(), HandlerResponseStatus.Created);
    }
}
