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
    }
}
