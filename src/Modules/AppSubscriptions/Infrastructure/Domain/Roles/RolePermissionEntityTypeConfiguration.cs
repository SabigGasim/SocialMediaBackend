using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles;

internal class RolePermissionEntityTypeConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.Permission)
            .WithMany()
            .HasForeignKey(x => x.PermissionId);

        var rolePermissionsToSeed = RolePermissionData
            .Mappings
            .SelectMany(mapping => mapping
                .Value
                .Select(permission => new RolePermission(roleId: mapping.Key.Id, permission.Id)
            ));

        builder.HasData(rolePermissionsToSeed);
    }
}
