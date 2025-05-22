using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations.GroupChats;

internal class GroupChatModeratorEntityTypeConfiguration : IEntityTypeConfiguration<GroupChatModerator>
{
    public void Configure(EntityTypeBuilder<GroupChatModerator> builder)
    {
        builder.HasKey(x => new {x.GroupChatId, x.ModeratorId});

        builder.ToTable("GroupChatModerators");

        builder.HasIndex(x => x.GroupChatId);

        builder.Property(x => x.GroupChatId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.Property(x => x.ModeratorId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.HasOne(x => x.GroupChat)
            .WithMany(x => x.Moderators)
            .HasForeignKey(x => x.GroupChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Moderator)
            .WithMany()
            .HasForeignKey(x => x.ModeratorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
