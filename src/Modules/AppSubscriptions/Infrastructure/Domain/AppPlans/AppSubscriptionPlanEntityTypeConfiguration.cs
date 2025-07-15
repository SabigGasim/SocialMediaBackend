using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.AppPlans;

internal class AppSubscriptionPlanEntityTypeConfiguration : IEntityTypeConfiguration<AppSubscriptionPlan>
{
    public void Configure(EntityTypeBuilder<AppSubscriptionPlan> builder)
    {
        builder.ToTable("AppSubscriptionPlans");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value));

        builder.HasOne(x => x.AppSubscriptionProduct)
            .WithMany(x => x.Plans)
            .HasForeignKey(x => x.AppSubscriptionProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Price, price =>
        {
            price.WithOwner();
            price.Property(p => p.PaymentInterval)
                .HasColumnName("PaymentInterval")
                .IsRequired();
            
            price.OwnsOne(p => p.MoneyValue, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("PriceAmount")
                    .IsRequired();
                money.Property(m => m.Currency)
                    .HasColumnName("PriceCurrency")
                    .IsRequired();
            });
        });
    }
}
