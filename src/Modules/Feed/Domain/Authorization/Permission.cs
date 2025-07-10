namespace SocialMediaBackend.Modules.Feed.Domain.Authorization;

public enum Permissions
{
    CreatePost, DeletePost, GetAllPosts,
    GetPost, LikePost, UnlikePost, 
    UpdatePost
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

    public Permissions Id { get; private set; }
    public string Name { get; private set; } = default!;

    private Permission() {}
    public Permission(Permissions permissionId, string name)
    {
        Id = permissionId;
        Name = name;
    }
}
