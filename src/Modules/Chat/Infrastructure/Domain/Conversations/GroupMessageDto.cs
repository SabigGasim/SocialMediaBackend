namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

public class GroupMessageDto
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; } = default!;
    public DateTimeOffset SentAt { get; set; }
}
