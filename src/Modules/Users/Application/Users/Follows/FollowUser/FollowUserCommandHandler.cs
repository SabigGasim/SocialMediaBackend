using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

internal sealed class FollowUserCommandHandler(
    UsersDbContext context,
    IUserContext userContext) : ICommandHandler<FollowUserCommand, FollowUserResponse>
{
    private readonly UsersDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<HandlerResponse<FollowUserResponse>> ExecuteAsync(FollowUserCommand command, CancellationToken ct)
    {
        var userToFollow = await _context.Users
            .Include(x => x.Followers)
            .FirstOrDefaultAsync(x => x.Id == command.UserToFollowId);

        if (userToFollow is null)
        {
            return ("User with the given Id was not found", HandlerResponseStatus.NotFound, command.UserToFollowId);
        }

        var result = userToFollow.FollowOrRequestFollow(_userContext.UserId);
        if (!result.IsSuccess)
        {
            return result;
        }

        var follow = result.Payload;

        _context.Add(follow);
        
        return (follow.MapToFollowResponse(), HandlerResponseStatus.Created);
    }
}
