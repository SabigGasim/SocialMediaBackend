using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations.GroupChats;

internal class UserGroupChatEntityTypeConfiguration : IEntityTypeConfiguration<UserGroupChat>
{
    public void Configure(EntityTypeBuilder<UserGroupChat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.GroupChat)
            .WithMany()
            .HasForeignKey(x => x.GroupChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Chatter)
            .WithMany()
            .HasForeignKey(x => x.ChatterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.IsJoined)
            .HasDefaultValue(false);
    }
}
