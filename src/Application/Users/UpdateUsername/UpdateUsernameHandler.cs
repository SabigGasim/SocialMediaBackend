using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Services;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.UpdateUsername;

public class UpdateUsernameHandler : ICommandHandler<UpdateUsernameCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserExistsChecker _userExistsChecker;

    public UpdateUsernameHandler(ApplicationDbContext context, IUserExistsChecker userExistsChecker)
    {
        _context = context;
        _userExistsChecker = userExistsChecker;
    }

    public async Task<HandlerResponse> ExecuteAsync(UpdateUsernameCommand command, CancellationToken ct)
    {
        var user = await _context.Users.FindAsync([command.UserId], ct);
        if (user is null)
        {
            return ("No user exists with the given Id", HandlerResponseStatus.NotFound, command.UserId);
        }

        var usernameIsModified = await user.ChangeUsernameAsync(command.Username, _userExistsChecker);
        if(!usernameIsModified)
        {
            return ("Username was not modified", HandlerResponseStatus.BadRequest, command.Username);
        }

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Modified;
    }
}
