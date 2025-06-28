namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? Error { get; set; }
    public bool Processed { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }
}
