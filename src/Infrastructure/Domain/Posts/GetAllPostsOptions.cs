namespace SocialMediaBackend.Infrastructure.Domain.Posts;

public class GetAllPostsOptions
{
    public string? IdOrUsername { get; set; }
    public string? Text { get; set; }
    public DateOnly? Since { get; set; }
    public DateOnly? Until { get; set; }
    public Order Order { get; set; }
    public required int Page { get; set; }
    public required int PageSize { get; set; }
}

public enum Order
{
    Unordered,
    Ascending,
    Descending
}
