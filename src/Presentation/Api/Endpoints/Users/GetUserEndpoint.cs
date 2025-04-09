using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Contracts.Responses;
using SocialMediaBackend.Application.Users.GetUser;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.Get), AllowAnonymous]
internal class GetUserEndpoint : RequestEndpoint<GetUserRequest, GetUserResponse>
{
    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        await HandleRequestAsync(new GetUserQuery(req.IdOrUsername), ct);
    }
}

internal class GetUserEndpointSummary : Summary<GetUserEndpoint>
{
    public GetUserEndpointSummary()
    {
        Summary = "Gets a user with the specified id or username from the system";
        Description = "Gets a user with the specified id or username from the system";
        Response<GetUserResponse>(200, "A user with the given id or username was returned");
        Response<ValidationFailureResponse>(400, "The given request parameter isn't valid for a username or an id");
        Response(404, "No user with the given id or username was found");
    }
}
