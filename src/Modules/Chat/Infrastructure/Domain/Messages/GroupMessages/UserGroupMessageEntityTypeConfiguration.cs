using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Messages.GroupMessages;

internal class UserGroupMessageEntityTypeConfiguration : IEntityTypeConfiguration<UserGroupMessage>
{
    public void Configure(EntityTypeBuilder<UserGroupMessage> builder)
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
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.GroupMessage)
            .WithMany()
            .HasForeignKey(x => x.GroupMessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.UserGroupChat)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.UserGroupChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
