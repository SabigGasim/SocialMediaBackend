using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints;

[HttpPost(ApiEndpoints.Users.Create), AllowAnonymous]
public class CreateUserEndpoint(IUsersModule module) : RequestEndpoint<CreateUserRequest, CreateUserResponse>(module)
{
    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var command = new CreateUserCommand(
            req.Username,
            req.Nickname,
            req.DateOfBirth,
            req.ProfilePicture);

        await HandleCommandAsync(command, ct);
    }
}