using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Messaging.Outbox;

internal class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Processed);
    }
}
