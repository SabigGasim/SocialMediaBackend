﻿namespace SocialMediaBackend.Modules.Users.Sdk;

internal static class ApiEndpoints
{
    private const string ApiBase = "/api";

    public static class Users
    {
        private const string Base = $"{ApiBase}/users";
        private const string UserActionBase = $"{Base}/{{UserId}}";

        public const string Me = $"{Base}/me";
        public const string Create = Base;
        public const string Get = $"{Base}/{{IdOrUsername}}";
        public const string GetAll = Base;
        public const string Delete = UserActionBase;
        public const string PatchUsername = $"{Me}/username";
        public const string PatchNickname = $"{Me}/nickname";

        public const string Follow = $"{UserActionBase}/followers";
        public const string Unfollow = $"{UserActionBase}/followers";
        public const string AcceptFollow = $"{Me}/followers/{{UserId}}/accept";
        public const string RejectFollow = $"{Me}/followers/{{UserId}}/reject";

        public static class Privacy
        {
            private const string PrivacyBase = $"{Me}/privacy";

            public const string ChangeProfileVisibility = $"{PrivacyBase}/visibility";
        }
    }

    public static class Posts
    {
        private const string Base = $"{ApiBase}/posts";

        public const string Create = Base;
        public const string Get = $"{Base}/{{PostId}}";
        public const string GetAll = Base;
        public const string Delete = $"{Base}/{{PostId}}";
        public const string Patch = $"{Base}/{{PostId}}";

        public const string Comment = $"{Base}/{{PostId}}/comments";
        public const string DeleteComment = $"{Base}/{{PostId}}/comments/{{CommentId}}";
        public const string GetAllPostComments = $"{Base}/{{PostId}}/comments";

        public const string Like = $"{Base}/{{PostId}}/likes";
        public const string Unlike = $"{Base}/{{PostId}}/likes";
    }

    public static class Comments
    {
        public const string Base = $"{ApiBase}/comments";

        public const string GetUserComments = $"{Base}/me";
        public const string Get = $"{Base}/{{CommentId}}";
        public const string Patch = $"{Base}/{{CommentId}}";

        public const string Reply = $"{Base}/{{ParentId}}";
        public const string GetReplies = $"{Reply}/replies";

        public const string Like = $"{Base}/{{CommentId}}/likes";
        public const string Unlike = $"{Base}/{{CommentId}}/likes";
    }
}
