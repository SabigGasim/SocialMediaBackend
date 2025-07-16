using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Subscriptions;

internal class SubscriptionRenewalPaymentEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionRenewalPayment>
{
    public void Configure(EntityTypeBuilder<SubscriptionRenewalPayment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.Payer)
            .WithMany(x => x.SubscriptionRenewalPayments)
            .HasForeignKey(x => x.PayerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Subscription)
            .WithMany()
            .HasForeignKey(x => x.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
