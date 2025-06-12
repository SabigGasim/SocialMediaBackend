namespace SocialMediaBackend.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

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

    public static class DirectChat
    {
        public const string Base = $"{ApiBase}/chats/direct";

        public const string CreateDirectChat = Base;
        public const string SendMessage = $"{Base}/{{ChatId}}";
        public const string DeleteMessageForMe = $"{Base}/{{ChatId}}/messages/{{MessageId}}/delete-for-me";
        public const string DeleteMessageForEveryone = $"{Base}/{{ChatId}}/messages/{{MessageId}}/delete-for-everyone";
        public const string GetAllMessages = $"{Base}/{{ChatId}}/messages";

        public const string SetMessageAsRead = $"{Base}/{{ChatId}}/messages/{{MessageId}}";
    }

    public static class GroupChat
    {
        public const string Base = $"{ApiBase}/chats/groups";

        public const string CreateGroupChat = Base;
        public const string SendMessage = $"{Base}/{{ChatId}}";
        public const string DeleteMessageForMe = $"{Base}/{{ChatId}}/messages/{{MessageId}}/delete-for-me";
        public const string DeleteMessageForEveryone = $"{Base}/{{ChatId}}/messages/{{MessageId}}/delete-for-everyone";
        public const string GetAllMessages = $"{Base}/{{ChatId}}/messages";

        public const string SetMessageAsRead = $"{Base}/{{ChatId}}/messages/mark-as-read";
        public const string MarkMessageAsReceived = $"{Base}/{{ChatId}}/messages/{{MessageId}}/mark-as-received";
        public const string KickGroupMember = $"{Base}/{{ChatId}}/members/{{MemberId}}/kick";
        public const string PromoteMember = $"{Base}/{{ChatId}}/members/{{MemberId}}/promote";

        public const string JoinGroupChat = $"{Base}/{{ChatId}}/join";
        public const string LeaveGroupChat = $"{Base}/{{ChatId}}/leave";

    }

    public static class ChatHub
    {
        public const string Base = $"{ApiBase}/chathub";
        public const string Connect = $"{Base}/connect";
    }

    public static class Payments
    {
        private const string Base = $"{ApiBase}/payments";
        public const string GetPayer = $"{Base}/payers/{{PayerId}}";
    }
}
