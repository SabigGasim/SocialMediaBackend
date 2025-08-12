namespace SocialMediaBackend.BuildingBlocks.Domain;

public enum AuthSuccessReason
{
    Forbidden, ResourceOwner, Admin, Authorized
}

public sealed class AuthResult
{
    public bool IsAuthorized { get; }
    public AuthSuccessReason SuccessReason { get; }

    private AuthResult(bool isAuthorized)
    {
        IsAuthorized = isAuthorized;
        SuccessReason = isAuthorized 
            ? AuthSuccessReason.Authorized
            : AuthSuccessReason.Forbidden;
    }

    private AuthResult(bool isAuthorized, AuthSuccessReason successReason)
    {
        IsAuthorized = isAuthorized;
        SuccessReason = successReason;
    }

    public static AuthResult ResourceOwner() => new(true, AuthSuccessReason.ResourceOwner);
    public static AuthResult Admin() => new(true, AuthSuccessReason.Admin);
    public static AuthResult Success() => new(true);
    public static AuthResult Fail() => new(false);


    public static implicit operator bool(AuthResult result) => result.IsAuthorized;
}
