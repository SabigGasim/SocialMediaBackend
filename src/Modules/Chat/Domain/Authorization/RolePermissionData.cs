namespace SocialMediaBackend.Modules.Chat.Domain.Authorization;

public static class RolePermissionData
{
    private static readonly Dictionary<Role, Permission[]> _mappings = [];

    public static IReadOnlyDictionary<Role, Permission[]> Mappings => _mappings.AsReadOnly();

    static RolePermissionData()
    {
        _mappings[Role.Chatter] = [
            Permission.CreateDirectChat,
            Permission.CreateDirectMessage,
            Permission.DeleteDirectMessageForEveryone,
            Permission.DeleteDirectMessageForMe,
            Permission.GetAllDirectMessages,
            Permission.MarkDirectMessageAsSeen,
            Permission.CreateGroupChat,
            Permission.JoinGroupChat,
            Permission.KickGroupMember,
            Permission.LeaveGroupChat,
            Permission.PromoteMember,
            Permission.CreateGroupMessage,
            Permission.DeleteGroupMessageForEveryone,
            Permission.DeleteGroupMessageForMe,
            Permission.GetAllGroupMessages,
            Permission.MarkGroupMessageAsSeen,
            Permission.MarkGroupMessageAsReceived,
        ];

        _mappings[Role.BasicPlan] = [];
        _mappings[Role.PlusPlan] = [.. _mappings[Role.BasicPlan]];

        _mappings[Role.AdminChatter] = [
            .._mappings[Role.Chatter],
            .._mappings[Role.PlusPlan]
        ];
    }
}
