using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations;

public class DirectMessageDto
{
    public Guid MessageId { get; set; }
    public Guid SenderId { get; set; }
    public string Text { get; set; } = default!;
    public DateTimeOffset SentAt { get; set; }
    public MessageStatus? Status { get; set; }
}
