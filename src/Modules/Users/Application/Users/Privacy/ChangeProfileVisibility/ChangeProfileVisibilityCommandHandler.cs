﻿using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Privacy.ChangeProfileVisibility;

internal sealed class ChangeProfileVisibilityCommandHandler(UsersDbContext context)
    : ICommandHandler<ChangeProfileVisibilityCommand>
{
    private readonly UsersDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(ChangeProfileVisibilityCommand command, CancellationToken ct)
    {
        var user = await _context.Users
            .Include(x => x.Followers.Where(x => x.Status == FollowStatus.Pending))
            .FirstAsync(x => x.Id == new UserId(command.UserId), ct);

        var result = user.ChangeProfilePrivacy(command.IsPublic);
        if (!result.IsSuccess)
        {
            return result;
        }

        return HandlerResponseStatus.NoContent;
    }
}
