using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Messages.DirectMessages;

internal class UserDirectMessageEntityTypeConfiguration : IEntityTypeConfiguration<UserDirectMessage>
{
    public void Configure(EntityTypeBuilder<UserDirectMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.DirectMessage)
            .WithMany()
            .HasForeignKey(x => x.DirectMessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.UserDirectChat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.UserDirectChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
