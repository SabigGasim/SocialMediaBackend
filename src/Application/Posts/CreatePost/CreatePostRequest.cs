using SocialMediaBackend.Application.Contracts;

namespace SocialMediaBackend.Application.Posts.CreatePost;

public record CreatePostRequest(string Text, IEnumerable<MediaRequest> MediaItems);
