﻿namespace SocialMediaBackend.Modules.Feed.Domain.Authorization;

public static class RolePermissionData
{
    private static readonly Dictionary<Role, Permission[]> _mappings = [];

    public static IReadOnlyDictionary<Role, Permission[]> Mappings => _mappings.AsReadOnly();

    static RolePermissionData()
    {
        _mappings[Role.Author] = [
            Permission.GetPost,
            Permission.CreatePost,
            Permission.GetAllPosts,
            Permission.DeletePost,
            Permission.UpdatePost,
            Permission.LikePost,
            Permission.UnlikePost,
            Permission.CreateComment,
            Permission.UpdateComment,
            Permission.DeleteComment,
            Permission.GetComment,
            Permission.GetAllPostComments,
            Permission.GetAllReplies,
            Permission.ReplyToComment,
            Permission.LikeComment,
            Permission.UnlikeComment
        ];

        _mappings[Role.BasicPlan] = [];
        _mappings[Role.PlusPlan] = [.. _mappings[Role.BasicPlan]];

        _mappings[Role.AdminAuthor] = [
            .._mappings[Role.Author],
            .._mappings[Role.PlusPlan]
        ];
    }
}
