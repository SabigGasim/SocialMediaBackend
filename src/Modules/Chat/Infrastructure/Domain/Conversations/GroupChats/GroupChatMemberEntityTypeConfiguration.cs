using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Conversations.GroupChats;

internal class GroupChatMemberEntityTypeConfiguration : IEntityTypeConfiguration<GroupChatMember>
{
    public void Configure(EntityTypeBuilder<GroupChatMember> builder)
    {
        builder.HasKey(x => new { x.GroupChatId, x.MemberId });

        builder.ToTable("GroupChatMembers");

        builder.HasIndex(x => x.GroupChatId);

        builder.Property(x => x.GroupChatId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.Property(x => x.MemberId)
            .HasConversion(
                id => id.Value,
                value => new(value)
        );

        builder.HasOne(x => x.GroupChat)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.GroupChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
