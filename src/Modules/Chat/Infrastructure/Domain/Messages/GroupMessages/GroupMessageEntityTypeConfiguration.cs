using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Messages.GroupMessages;

internal class GroupMessageEntityTypeConfiguration : IEntityTypeConfiguration<GroupMessage>
{
    public void Configure(EntityTypeBuilder<GroupMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Chat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.SeenBy)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "GroupMessage_SeenBy",
                right => right.HasOne<Chatter>()
                      .WithMany()
                      .HasForeignKey("ChatterId")
                      .OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<GroupMessage>()
                    .WithMany()
                    .HasForeignKey("GroupMessageId")
                    .OnDelete(DeleteBehavior.Cascade))
            .HasKey("GroupMessageId", "ChatterId");
    }
}
