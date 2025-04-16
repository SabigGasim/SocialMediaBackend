namespace SocialMediaBackend.Api.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Users
    {
        private const string Base = $"{ApiBase}/users";
        private const string UserActionBase = $"{Base}/{{UserId}}";
        private const string PatchBase = UserActionBase;

        public const string Create = Base;
        public const string Get = $"{Base}/{{IdOrUsername}}";
        public const string GetAll = Base;
        public const string Delete = UserActionBase;
        public const string PatchUsername = $"{PatchBase}/username";
        public const string PatchNickname = $"{PatchBase}/nickname";
    }

    public static class Posts
    {
        private const string Base = $"{ApiBase}/posts";

        public const string Create = Base;
        public const string Get = $"{Base}/{{PostId}}";
        public const string GetAll = Base;
        public const string Delete = $"{Base}/{{PostId}}";
    }
}
