using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles;

internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Name);

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Name)
            .IsUnique();

        builder.Property(p => p.Name)
            .IsRequired();

        builder.HasMany(x => x.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();

        builder.HasMany(x => x.Users)
            .WithMany()
            .UsingEntity<UserRole>(            // EF Core can't easily infer this UserRole join table
                l => l.HasOne(ur => ur.User)
                      .WithMany()
                      .HasForeignKey(ur => ur.UserId),
                r => r.HasOne(ur => ur.Role)
                      .WithMany()
                      .HasForeignKey(ur => ur.RoleId),
                j =>
                {
                    j.HasKey(ur => new { ur.RoleId, ur.UserId });
                    j.ToTable("UserRoles");
                });

        var rolesToSeed = RolePermissionData
            .Mappings
            .Select(mapping => mapping.Key);

        builder.HasData(rolesToSeed);
    }
}
