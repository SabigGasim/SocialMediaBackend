using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Subscriptions;

internal class SubscriptionPaymentEntityTypeConfiguration : IEntityTypeConfiguration<SubscriptionPayment>
{
    public void Configure(EntityTypeBuilder<SubscriptionPayment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.Payer)
            .WithMany(x => x.SubscriptionPayments)
            .HasForeignKey(x => x.PayerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.AppSubscriptionPlan)
            .WithMany()
            .HasForeignKey(x => x.AppSubscriptionPlanId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
