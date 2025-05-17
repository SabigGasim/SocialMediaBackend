using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations.GroupChats;

internal class GroupChatEntityTypeConfiguration : IEntityTypeConfiguration<GroupChat>
{
    public void Configure(EntityTypeBuilder<GroupChat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasMany(x => x.Chatters)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "GroupChat_Chatters",
                right => right.HasOne<Chatter>()
                      .WithMany()
                      .HasForeignKey("ChatterId")
                      .OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<GroupChat>()
                    .WithMany()
                    .HasForeignKey("GroupChatId")
                    .OnDelete(DeleteBehavior.Cascade))
            .HasKey("GroupChatId", "ChatterId");

        builder.HasMany(x => x.Moderators)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "GroupChat_Moderators",
                right => right.HasOne<Chatter>()
                      .WithMany()
                      .HasForeignKey("ModeratorId")
                      .OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<GroupChat>()
                    .WithMany()
                    .HasForeignKey("GroupChatId")
                    .OnDelete(DeleteBehavior.Cascade))
            .HasKey("GroupChatId", "ModeratorId");

        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
