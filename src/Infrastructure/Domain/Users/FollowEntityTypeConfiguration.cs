using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaBackend.Domain.Users.Follows;

namespace SocialMediaBackend.Infrastructure.Domain.Users;

public class FollowEntityTypeConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.Ignore(x => x.Id);

        builder.HasKey(x => new { x.FollowerId, x.FollowingId } )
            .HasName("Id");

        builder.Property(x => x.FollowerId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.Property(x => x.FollowingId)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        builder.HasOne(x => x.Follower)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Following)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.FollowingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
