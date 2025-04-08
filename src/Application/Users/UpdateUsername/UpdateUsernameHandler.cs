using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.UpdateUsername;

public class UpdateUsernameHandler : ICommandHandler<UpdateUsernameCommand>
{
    private readonly ApplicationDbContext _context;

    public UpdateUsernameHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HandlerResponse> ExecuteAsync(UpdateUsernameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync(command.UserId);
        if (user is null)
        {
            return HandlerResponse.CreateError("Bad request: no user exists with the given Id", command.UserId);
        }

        if(user.Username == command.Username)
        {
            return HandlerResponse.CreateError($"Bad request: username is already {command.Username}");
        }

        var usernameIsAlreadyTaken = await _context.Users
            .AsNoTracking()
            .AnyAsync(x => x.Username == command.Username);

        if (usernameIsAlreadyTaken)
        {
            return HandlerResponse.CreateError("Username is already taken.", command.UserId);
        }

        user.ChangeUsername(command.Username);
        await _context.SaveChangesAsync();

        return HandlerResponse.CreateSuccess();
    }
}
