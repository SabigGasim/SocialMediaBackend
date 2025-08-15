using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace SocialMediaBackend.Api.Authentication;

[AllowAnonymous]
[HttpPost(ApiEndpoints.Auth.Token)]
public class TokenEndpoint(IJwtProvider jwtProvder) : Endpoint<TokenGenerationRequest, string>
{
    private readonly IJwtProvider _jwtProvder = jwtProvder;

    public override async Task HandleAsync(TokenGenerationRequest req, CancellationToken ct)
    {
        var token = _jwtProvder.GenerateToken(req);

        await Send.OkAsync(token, ct);
    }
}
