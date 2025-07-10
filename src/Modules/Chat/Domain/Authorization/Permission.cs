namespace SocialMediaBackend.Modules.Chat.Domain.Authorization;

public enum Permissions
{
    CreateDirectChat, CreateDirectMessage, DeleteDirectMessageForEveryone,
    DeleteDirectMessageForMe, GetAllDirectMessages, MarkDirectMessageAsSeen,
    CreateGroupChat, CreateGroupMessage, DeleteGroupMessageForEveryone,
    DeleteGroupMessageForMe, GetAllGroupMessages, JoinGroupChat,
    KickGroupMember, LeaveGroupChat, MarkGroupMessageAsSeen,
    MarkGroupMessageAsReceived, PromoteMember
}

public class Permission
{
    public static readonly Permission CreateDirectChat = new(Permissions.CreateDirectChat, "Permissions.DirectChat.Create");
    public static readonly Permission CreateDirectMessage = new(Permissions.CreateDirectMessage, "Permissions.DirectMessage.Create");
    public static readonly Permission DeleteDirectMessageForEveryone = new(Permissions.DeleteDirectMessageForEveryone, "Permissions.DirectMessage.DeleteForEveryone");
    public static readonly Permission DeleteDirectMessageForMe = new(Permissions.DeleteDirectMessageForMe, "Permissions.DirectMessage.DeleteForMe");
    public static readonly Permission GetAllDirectMessages = new(Permissions.GetAllDirectMessages, "Permissions.DirectMessage.GetAll");
    public static readonly Permission MarkDirectMessageAsSeen = new(Permissions.MarkDirectMessageAsSeen, "Permissions.DirectMessage.MarkAsSeen");
    public static readonly Permission CreateGroupChat = new(Permissions.CreateGroupChat, "Permissions.GroupChat.Create");
    public static readonly Permission JoinGroupChat = new(Permissions.JoinGroupChat, "Permissions.GroupChat.Join");
    public static readonly Permission KickGroupMember = new(Permissions.KickGroupMember, "Permissions.GroupChat.KickMember");
    public static readonly Permission LeaveGroupChat = new(Permissions.LeaveGroupChat, "Permissions.GroupChat.Leave");
    public static readonly Permission PromoteMember = new(Permissions.PromoteMember, "Permissions.GroupChat.PromoteMember");
    public static readonly Permission CreateGroupMessage = new(Permissions.CreateGroupMessage, "Permissions.GroupMessage.Create");
    public static readonly Permission DeleteGroupMessageForEveryone = new(Permissions.DeleteGroupMessageForEveryone, "Permissions.GroupMessage.DeleteForEveryone");
    public static readonly Permission DeleteGroupMessageForMe = new(Permissions.DeleteGroupMessageForMe, "Permissions.GroupMessage.DeleteForMe");
    public static readonly Permission GetAllGroupMessages = new(Permissions.GetAllGroupMessages, "Permissions.GroupMessage.GetAll");
    public static readonly Permission MarkGroupMessageAsSeen = new(Permissions.MarkGroupMessageAsSeen, "Permissions.GroupMessage.MarkAsSeen");
    public static readonly Permission MarkGroupMessageAsReceived = new(Permissions.MarkGroupMessageAsReceived, "Permissions.GroupMessage.MarkAsReceived");

    public Permissions Id { get; private set; }
    public string Name { get; private set; } = default!;

    private Permission() {}
    public Permission(Permissions permissionId, string name)
    {
        Id = permissionId;
        Name = name;
    }
}
