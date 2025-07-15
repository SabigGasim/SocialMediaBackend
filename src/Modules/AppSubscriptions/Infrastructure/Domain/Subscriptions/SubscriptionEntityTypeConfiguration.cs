using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Subscriptions;

internal class SubscriptionEntityTypeConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.Subscriber)
            .WithOne(x => x.Subscription)
            .HasForeignKey<Subscription>(x => x.SubscriberId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
