namespace SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

public record CreatePostRequest(string Text, IEnumerable<string> MediaItems);
