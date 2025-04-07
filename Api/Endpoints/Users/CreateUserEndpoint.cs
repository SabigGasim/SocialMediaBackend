using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.CreateUser;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpPost(ApiEndpoints.Users.Create), AllowAnonymous]
public class CreateUserEndpoint : RequestEndpoint<CreateUserRequest, CreateUserResponse>
{
    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var command = new CreateUserCommand(
            req.Username,
            req.Nickname,
            req.DateOfBirth,
            req.ProfilePicture);

        await HandleRequestAsync(command, ct);
    }
}
