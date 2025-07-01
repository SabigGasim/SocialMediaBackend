using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations.DirectChats;

internal class DirectChatEntityTypeConfiguration : IEntityTypeConfiguration<DirectChat>
{
    public void Configure(EntityTypeBuilder<DirectChat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.FirstChatter)
            .WithMany()
            .HasForeignKey(x => x.FirstChatterId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.SecondChatter)
            .WithMany()
            .HasForeignKey(x => x.SecondChatterId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
