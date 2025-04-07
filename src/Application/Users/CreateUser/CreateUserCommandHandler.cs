using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Services;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IUserExistsChecker _userExistsChecker;
    private readonly ApplicationDbContext _context;

    public CreateUserCommandHandler(IUserExistsChecker userExistsChecker, ApplicationDbContext context)
    {
        _userExistsChecker = userExistsChecker;
        _context = context;
    }

    public async Task<HandlerResponse<CreateUserResponse>> ExecuteAsync(CreateUserCommand command, CancellationToken ct)
    {
        var user = await User.CreateAsync(command.Username, command.Nickname, command.DateOfBirth,
            _userExistsChecker, command.ProfilePicture);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.MapToResponse();
    }
}
