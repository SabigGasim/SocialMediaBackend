using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.AppPlans;

public class AppSubscriptionProductEntityTypeConfiguration : IEntityTypeConfiguration<AppSubscriptionProduct>
{
    public void Configure(EntityTypeBuilder<AppSubscriptionProduct> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
                );

        builder.Property(x => x.Tier)
            .HasColumnName("Tier")
            .IsRequired();

        builder.OwnsMany(x => x.Plans, m =>
        {
            m.WithOwner().HasForeignKey("ProductId");
            
            m.HasKey(x => x.Id);

            m.Property(x => x.Id)
                .HasConversion(
                    id => id.Value,
                    value => new(value)
                    );

            m.OwnsOne(x => x.Price, price =>
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
        });
    }
}
