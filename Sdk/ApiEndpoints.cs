namespace SocialMediaBackend.Sdk;

internal static class ApiEndpoints
{
    private const string ApiBase = "/api";

    internal static class Users
    {
        private const string Base = $"{ApiBase}/users";

        public const string Create = Base;
    }
}
