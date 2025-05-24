namespace SocialMediaBackend.Modules.Chat.Domain;

public static class ChatHubMethods
{
    public const string SendDirectMessage = "SendDirectMessage";
    public const string DeleteDirectMessage = "DeleteDirectMessage";
    public const string UserConnected = "UserConnected";
    public const string UserDisconnected = "UserDisconnected";
    public const string ReceiveGroupChatCreated = "ReceiveGroupChatCreated";
    public const string ReceiveGroupMessage = "ReceiveGroupMessage";
    public const string DeleteGroupMessage = "DeleteGroupMessage";
    public const string KickGroupMember = "KickGroupMember";
    public const string ReceiveGroupJoined = "ReceiveGroupJoined";
}