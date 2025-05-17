using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations.DirectChats;

internal class UserDirectChatEntityTypeConfiguration : IEntityTypeConfiguration<UserDirectChat>
{
    public void Configure(EntityTypeBuilder<UserDirectChat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.Chatter)
            .WithMany()
            .HasForeignKey(x => x.ChatterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.DirectChat)
            .WithMany()
            .HasForeignKey(x => x.DirectChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
