namespace SocialMediaBackend.Modules.Feed.Domain.Authorization;

public enum Permissions
{
    CreatePost, DeletePost, GetAllPosts,
    GetPost, LikePost, UnlikePost,
    UpdatePost, CreateComment, UpdateComment,
    DeleteComment, GetComment, GetAllPostComments,
    ReplyToComment, GetAllReplies, LikeComment,
    UnlikeComment
}

public class Permission
{
    public static readonly Permission CreatePost = new(Permissions.CreatePost, "Permissions.Posts.Create");
    public static readonly Permission DeletePost = new(Permissions.DeletePost, "Permissions.Posts.Delete");
    public static readonly Permission GetAllPosts = new(Permissions.GetAllPosts, "Permissions.Posts.GetAll");
    public static readonly Permission GetPost = new(Permissions.GetPost, "Permissions.Posts.Get");
    public static readonly Permission LikePost = new(Permissions.LikePost, "Permissions.Posts.Like");
    public static readonly Permission UnlikePost = new(Permissions.UnlikePost, "Permissions.Posts.Unlike");
    public static readonly Permission UpdatePost = new(Permissions.UpdatePost, "Permissions.Posts.Update");
    public static readonly Permission CreateComment = new(Permissions.CreateComment, "Permissions.Comments.Create");
    public static readonly Permission UpdateComment = new(Permissions.UpdateComment, "Permissions.Comments.Update");
    public static readonly Permission DeleteComment = new(Permissions.DeleteComment, "Permissions.Comments.Delete");
    public static readonly Permission GetComment = new(Permissions.GetComment, "Permissions.Comments.Get");
    public static readonly Permission GetAllPostComments = new(Permissions.GetAllPostComments, "Permissions.Comments.GetAll");
    public static readonly Permission ReplyToComment = new(Permissions.ReplyToComment, "Permissions.Comments.Reply");
    public static readonly Permission GetAllReplies = new(Permissions.GetAllReplies, "Permissions.Comments.GetAllReplies");
    public static readonly Permission LikeComment = new(Permissions.LikeComment, "Permissions.Comments.Like");
    public static readonly Permission UnlikeComment = new(Permissions.UnlikeComment, "Permissions.Comments.Unlike");

    public Permissions Id { get; private set; }
    public string Name { get; private set; } = default!;

    private Permission() {}
    public Permission(Permissions permissionId, string name)
    {
        Id = permissionId;
        Name = name;
    }
}
