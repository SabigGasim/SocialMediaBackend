using SocialMediaBackend.Modules.Users.Application.Contracts;

namespace SocialMediaBackend.Modules.Users.Application.Posts.CreatePost;

public record CreatePostRequest(string Text, IEnumerable<MediaRequest> MediaItems);
